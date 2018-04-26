using be4care.Helpers;
using be4care.Services;
using BottomBar.XamarinForms;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace be4care.PageModels
{
    class SplashPageModel : FreshMvvm.FreshBasePageModel
    {
        private IDialogService _dialogServices;
        public SplashPageModel(IDialogService _dialogServices)
        {
            this._dialogServices = _dialogServices;
        }
        public async override void Init(object initData)
        {
            base.Init(initData);
            var auth = Settings.AuthToken;

            //simulate intialise api 
            await System.Threading.Tasks.Task.Delay(3000);
            if (!CrossConnectivity.Current.IsConnected)
            {
                 _dialogServices.ShowMessage("verifier votre connection internet", "Erreur", null, false);
            }
            // if (string.IsNullOrEmpty(auth))
            //await CoreMethods.PushPageModel<onBoardingPageModel>();
           // else
                //    await CoreMethods.PushPageModel<InscriptionPageModel>();
                    //RaisePageWasPopped();
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Orange, BarBackgroundColor = Color.White };
                    tabs.AddTab<DocumentPageModel>("Documents", "doc.png");
                    tabs.AddTab<SearchPageModel>("Recherche", "search.png");
                    tabs.AddTab<AddDocPageModel>("", "plus.png");
                    tabs.AddTab<FavPageModel>("Raccourcis", "favorite.png");
                    tabs.AddTab<AccountPageModel>("Mon  Compte", "account.png");

                    App.Current.MainPage = tabs;
                }
                else
                
                {
                     var bottomBarPage = new CustomNavigation() { BarTextColor = Color.Orange, BarBackgroundColor = Color.White };

                     bottomBarPage.FixedMode = true;
                     bottomBarPage.BarTheme = BottomBarPage.BarThemeTypes.Light;
                     bottomBarPage.BarTextColor = Color.Orange;

                     bottomBarPage.AddTab<DocumentPageModel>("Documents", "doc.png");
                     bottomBarPage.AddTab<SearchPageModel>("Recherche", "search.png");
                     bottomBarPage.AddTab<AddDocPageModel>("", "plus.png");
                     bottomBarPage.AddTab<FavPageModel>("Raccourcis", "favorite.png");
                     bottomBarPage.AddTab<AccountPageModel>("Mon Compte", "account.png");

                     App.Current.MainPage = bottomBarPage;
                }
            }

           
        }
        

    }
}
