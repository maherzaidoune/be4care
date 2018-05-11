using be4care.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
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

        private string _number { get; set; }
        public string name { get; set; }
        public string  spec { get; set; }
        public List<detail> contact { get; set; }

        public ICommand call => new Command(makeCall);
        public ICommand email => new Command(sendEmail);

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

        public ContactDetailsPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            var c = initData as Contact;

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
            }else if (c is HealthStruct)
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
