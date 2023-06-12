using AaronMorris_C971_Task1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AaronMorris_C971_Task1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        async void LoadSampleData_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();

            if (Settings.FirstRun)
            {

              DatabaseService.LoadOnFirstRun();
              Settings.FirstRun = false;

               await Navigation.PopToRootAsync();
            }
        }

        async void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            await DatabaseService.ClearDatabaseData();
            
            await Navigation.PushAsync(new TermsPage());
        }
    }
}