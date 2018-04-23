using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace be4care.Pages.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowErrorPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        

        public string message { get; set; }
        public string title { get; set; }
        public String buttonText { get; set; }
        public bool hasButton { get; set; }

        public ShowErrorPage(string message, string title, string buttomText, bool hasButton)
        {
            InitializeComponent();
            BindingContext = new PageModels.ShowErrorPageModel();
            pagetitle.Text = title;
            msg.Text = message;
            button.IsVisible = hasButton;
            button.Text = buttomText;
        }
        
    }
}
