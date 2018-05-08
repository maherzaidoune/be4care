using be4care.Models;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class HstructListPageModel : FreshMvvm.FreshBasePageModel
    {
        [AddINotifyPropertyChangedInterface]
        public class hList
        {
            public bool add { get; set; }
            public HealthStruct hstruct { get; set; }
            public string fullName { get; set; }
        }
        private IRestServices _restServices;
        private IDoctorServices _doctorServices;
        private IHStructServices _hStructServices;
        public IList<hList> structs { get; set; }
        public IList<hList> list { get; set; }
        public IList<HealthStruct> userstruct { get; set; }
        private IDialogService _dialogServices;
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public hList selected { get; set; }

        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            Task.Run(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });
        }


        public string search { get; set; }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
            }
        }


        public ICommand refresh => new Command(refreshing);

        private void refreshing(object obj)
        {
            IsRefreshing = true;
            Task.Run(async () =>
            {
                var st = await _restServices.GetAllHstruct();

                if (st != null)
                {
                    _hStructServices.saveAllHstruct(st);
                    foreach (HealthStruct d in st)
                    {
                        if (userstruct != null && userstruct.Count > 0)
                        {
                            foreach (HealthStruct ud in userstruct)
                            {
                                if (d.id == ud.id)
                                {
                                    structs.Add(new hList { add = false, hstruct = d, fullName = d.fullName });
                                    break;
                                }
                            }
                            structs.Add(new hList { add = true, hstruct = d, fullName = d.fullName });
                        }
                        else
                        {
                            structs.Add(new hList { add = true, hstruct = d, fullName = d.fullName });
                        }
                    }
                }

                list = structs;
                IsRefreshing = false;

            });
        }



        public void OnsearchChanged()
        {
            if (string.IsNullOrEmpty(search))
            {
                list = structs;
            }
            else
            {
                list = structs.Where(d => d.fullName.Contains(search)).ToList();
            }

        }


        public ICommand addhStruct => new Command(addthisStruct);

        private void addthisStruct(object obj)
        {
            isEnabled = false;
            isBusy = true;
            Task.Run(async () =>
            {

                var doc = obj as hList;
                var d = doc.hstruct;
                if (await _restServices.AddHStructFromDb(d.id))
                {
                    await _hStructServices.SaveStruct(d);
                    MessagingCenter.Send(this, "newStructAdd");
                    _dialogServices.ShowMessage(d.fullName + " a été ajouter a votre liste de contact",false);

                }
                else
                {
                    _dialogServices.ShowMessage("Erreur : Veuillez réessayer plus tard",true);
                }
                isBusy = false;
                isEnabled = true;
                doc.add = false;
                //refreshing(null);
            });
        }




        public HstructListPageModel(IDialogService _dialogServices, IRestServices _restServices, IDoctorServices _doctorServices, IHStructServices _hStructServices)
        {
            this._restServices = _restServices;
            this._doctorServices = _doctorServices;
            this._hStructServices = _hStructServices;
            this._dialogServices = _dialogServices;
            structs = new List<hList>();
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            isBusy = false;
            Task.Run(async () =>
            {

                await UpdateList();
            });
        }


        public async Task UpdateList()
        {
            try
            {
                userstruct = await _hStructServices.GetStructs();
                if(userstruct == null || userstruct.Count== 0)
                {
                    userstruct = new List<HealthStruct>();
                }
            }
            catch
            {
                Console.WriteLine("can't get user doctor list");
                userstruct = null;
            }
            try
            {
                var docs = await _hStructServices.GetAllHstrcts();
                if (docs == null)
                {
                    docs = await _restServices.GetAllHstruct();
                    _hStructServices.saveAllHstruct(docs);
                }

                foreach (HealthStruct d in docs)
                {
                    if (userstruct != null && userstruct.Count > 0)
                    {
                        foreach (HealthStruct ud in userstruct)
                        {
                            if (d.id == ud.id)
                            {
                                structs.Add(new hList { add = false, hstruct = d, fullName = d.fullName });
                                break;
                            }
                        }
                        structs.Add(new hList { add = true, hstruct = d, fullName = d.fullName });
                    }
                    else
                    {
                        structs.Add(new hList { add = true, hstruct = d, fullName = d.fullName });
                    }
                }
            }
            catch
            {
                Console.WriteLine("error getting structs from local database");
                var docs = await _restServices.GetAllHstruct();
                if (_hStructServices.saveAllHstruct(docs))
                    Console.WriteLine("Doctors  saved");
                foreach (HealthStruct d in docs)
                {
                    foreach (HealthStruct ud in userstruct)
                    {
                        if (d.id == ud.id)
                        {
                            structs.Add(new hList { add = false, hstruct = d, fullName = d.fullName });
                            break;
                        }
                    }
                    structs.Add(new hList { add = true, hstruct = d, fullName = d.fullName });
                }

            }
            isBusy = false;
            isEnabled = true;
            list = structs;

        }

    }
}
