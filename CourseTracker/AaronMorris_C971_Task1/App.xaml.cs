using Xamarin.Forms;
using Xamarin.Essentials;
using AaronMorris_C971_Task1.Services;
using AaronMorris_C971_Task1.Views;

namespace AaronMorris_C971_Task1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //TODO comment out before submission
           // Preferences.Clear();

            if (Settings.FirstRun)
            {
                DatabaseService.LoadOnFirstRun();
                Settings.FirstRun = false;
            }

            var dashBoard = new Dashboard();
            var navPage = new NavigationPage(dashBoard);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}