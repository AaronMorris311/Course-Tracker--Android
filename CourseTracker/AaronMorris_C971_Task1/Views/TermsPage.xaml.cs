using AaronMorris_C971_Task1.Models;
using AaronMorris_C971_Task1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AaronMorris_C971_Task1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPage
    {
        public TermsPage()
        {
            InitializeComponent();

            ////removes flyout of courses when on terms page -- verify this comes back when going into courses page
            //MessagingCenter.Send<App, string>(App.Current as App, "OneMessage", "");

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            termCollectionView.ItemsSource = await DatabaseService.GetTerms();
        }

        

        //async void Courses_Button_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new CoursesPage());
        //}

        async void termCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Term term = (Term)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new CoursesPage(term.termNumber));
            }
        }

        async void AddTerm_Clicked_1(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AddTerm());
        }
    }
}