using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace be4care.PageModels
{
    class LoginPopupPageModel
    {
        public ICommand validate => new Command(login);
        private  void login(object obj)
        {
             Console.WriteLine(Services.RestServices.login(user, password));
        }

        public string user { get; set; }
        public string password { get; set; }
    }
}
