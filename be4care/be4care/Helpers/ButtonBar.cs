using be4care.PageModels;
using BottomBar.XamarinForms;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace be4care.Helpers
{
    class ButtonBar
    {
        public static void initBar()
        {
            {

                Task.Run(() =>
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Orange, BarBackgroundColor = Color.White };
                        tabs.AddTab<DocumentPageModel>("Documents", "doc.png");
                        tabs.AddTab<SearchPageModel>("Recherche", "search.png");
                        tabs.AddTab<AddDocPageModel>("", "plus.png");
                        tabs.AddTab<FavPageModel>("Raccourcis", "favorite.png");
                        tabs.AddTab<AccountPageModel>("Mon Compte", "account.png");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Current.MainPage = tabs;
                        });
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
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Current.MainPage = bottomBarPage;
                        });
                    }
                }).Wait();

            }
        }
    }
}
