using be4care.Models;
using be4care.PageModels;
using be4care.Services;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactEditPage : PopupPage
    {
        private IRestServices _restService;
        private IDoctorServices _doctorService;
        private IHStructServices _hstructService;
        public ContactEditPage (Contact contact)
		{
            _restService = new RestServices();
            _doctorService = new DoctorServices();
            _hstructService = new HStructServices();
			InitializeComponent ();
            BindingContext = new ContactEditPageModel(contact);
		}
	}
}