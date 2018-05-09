using PropertyChanged;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class TriOptionPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand tripardate => new Command(pardate);
        public ICommand triparproff => new Command(parproff);
        public ICommand tripartype => new Command(partype);
        public ICommand triparstruct => new Command(parstruct);
        public string parDate { get {
                return (trimode.Equals("date")) ?"#fb8c00" : "#000000";
            } }
        public string parProf
        {
            get
            {
                return (trimode.Equals("prof")) ?  "#fb8c00" : "#000000";
            }
        }
        public string parType
        {
            get
            {
                return (trimode.Equals("type")) ?  "#fb8c00" : "#000000";
            }
        }
        public string parStruct
        {
            get
            {
                return (trimode.Equals("struct")) ? "#fb8c00" : "#000000";
            }
        }



        public string trimode { get; set; } //0-date , 1-prof , 2-type , 3-struct

        private void parproff(object obj)
        {
            MessagingCenter.Send(this, "triepar", 1);
            Application.Current.Properties["trimode"] = "prof";
            PopupNavigation.Instance.PopAllAsync();
        }
        private void partype(object obj)
        {
            MessagingCenter.Send(this, "triepar", 2);
            Application.Current.Properties["trimode"] = "type";
            PopupNavigation.Instance.PopAllAsync();
        }
        private void parstruct(object obj)
        {
            MessagingCenter.Send(this, "triepar", 3);
            Application.Current.Properties["trimode"] = "struct";
            PopupNavigation.Instance.PopAllAsync();
        }
        private void pardate(object obj)
        {
            MessagingCenter.Send(this, "triepar",0);
            Application.Current.Properties["trimode"] = "date";
            PopupNavigation.Instance.PopAllAsync();
        }


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            
        }

        public TriOptionPageModel()
        {
            Console.WriteLine("PopUp tri option page constructor");
            if (Application.Current.Properties.ContainsKey("trimode"))
            {
                trimode = Application.Current.Properties["trimode"] as string;
            }
            if (string.IsNullOrEmpty(trimode))
                trimode = "date";
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Console.WriteLine("PopUp tri option page init");
            
        }





    }
}
