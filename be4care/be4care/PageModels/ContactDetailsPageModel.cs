using be4care.Models;
using be4care.Pages;
using be4care.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ContactDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        private string _email;

        [AddINotifyPropertyChangedInterface]
        public class detail
        {
            public string menu { get; set; }
            public string info { get; set; }
        }
        private IRestServices _restService;
        private IDoctorServices _doctorService;
        private IHStructServices _hstructService;
        private IDialogService _dialogService;
        private string _number { get; set; }
        public string name { get; set; }
        public string  spec { get; set; }
        public List<detail> contact { get; set; }
        public Contact c { get; set; }
        public ICommand call => new Command(makeCall);
        public ICommand email => new Command(sendEmail);
        public ICommand setting => new Command(editContact);

        private void editContact(object obj)
        {
            Console.WriteLine("edit contact called \n" );
            Device.BeginInvokeOnMainThread(async () =>
            {
                MessagingCenter.Subscribe<ContactEditPageModel>(this, "delete", delete);
                MessagingCenter.Subscribe<ContactEditPageModel>(this, "editcontact", edit);
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ContactEditPage(c));
            });
        }

        private void edit(ContactEditPageModel obj)
        {
            MessagingCenter.Unsubscribe<ContactEditPageModel>(this, "editcontact");
            Console.WriteLine("edit called  "+ obj.contact.fullName +"\n" );
            if(c != null)
            {
                if (c  is Doctor)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<AddDoctorPageModel>(c as Doctor);
                        RaisePropertyChanged();
                    });
                    
                }
                if (c is HealthStruct)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await CoreMethods.PushPageModel<AddHstructPageModel>(c as HealthStruct);
                        RaisePropertyChanged();
                    });
                    
                }
            }
            else
            {
                _dialogService.ShowMessage("Erreur", true);
            }
        }

        private void delete(ContactEditPageModel obj)
        {
            MessagingCenter.Unsubscribe<ContactEditPageModel>(this, "delete");
            if (c != null)
            {
                if (c is Doctor)
                {
                    if (_restService.DeleteDoctor(c as Doctor))
                    {
                        Task.Run(async () =>
                        {
                            if (await _doctorService.DeleteDoctor(c as Doctor))
                                MessagingCenter.Send(this, "delete");
                            else
                                MessagingCenter.Send(this, "deletefromserver");
                            _dialogService.ShowMessage(c.fullName + "supprimer avec succes", false);
                        });
                    }
                    else
                    {
                        _dialogService.ShowMessage("Erreur", true);
                    }
                }
                if (c is HealthStruct)
                {
                    if (_restService.DeleteHstruct(c as HealthStruct))
                    {
                        Task.Run(async () =>
                        {
                            if(await _hstructService.DeleteStruct(c as HealthStruct))
                                MessagingCenter.Send(this, "delete");
                            else
                                MessagingCenter.Send(this, "deletefromserver");
                            _dialogService.ShowMessage(c.fullName + "supprimer avec succes", false);
                        });
                    }
                    else
                    {
                        _dialogService.ShowMessage("Erreur", true);
                    }

                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<ContactPageModel>();
                //CoreMethods.RemoveFromNavigation();
                RaisePropertyChanged();
            });
        }

        private void sendEmail(object obj)
        {
            try
            {
                DependencyService.Get<ISendEmail>().send(_email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error sending email "+ex.Message);
            }
        }

        private void makeCall(object obj)
        {
            try
            {
                DependencyService.Get<IMakeCall>().Call(_number);
            }
            catch(Exception e)
            {
                Console.WriteLine("error  making phone call "+e.Message);
            }

        }

        public ICommand backClick => new Command(backClickbutton);

        private void backClickbutton(object obj)
        {
            CoreMethods.PopPageModel();
            RaisePropertyChanged();
        }

        public ContactDetailsPageModel(IRestServices _restService,IDoctorServices _doctorService,IHStructServices _hstructService, IDialogService _dialogService)
        {
            this._doctorService = _doctorService;
            this._hstructService = _hstructService;
            this._restService = _restService;
            this._dialogService = _dialogService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData == null)
                return;

            c = initData as Contact;
            if (c is Doctor)
            {
                var doc = c as Doctor;
                contact = new List<detail>
            {
                new detail{menu = "Adresse",info = doc.adress},
                new detail{menu = "Numéro de  Télephone ",info = doc.phNumber},
                new detail{menu = "Email Adresse",info = doc.email},
                new detail{menu = "Structure de Santé",info = doc.healthStruct},
                new detail{menu = "Note",info = doc.note}
            };
                name = doc.fullName;
                spec = doc.specialite;
                _number = doc.phNumber;
                _email = doc.email;
            }
            else if (c is HealthStruct)
            {
                var h = c as HealthStruct;
                contact = new List<detail>
            {
                new detail{menu = "Adresse",info = h.adress},
                new detail{menu = "Numéro de  Télephone ",info = h.phNumber},
                new detail{menu = "Email Adresse",info = h.email},
            };
                name = h.fullName;
                _number = h.phNumber;
                _email = h.email;
            }
        }
    }
}
