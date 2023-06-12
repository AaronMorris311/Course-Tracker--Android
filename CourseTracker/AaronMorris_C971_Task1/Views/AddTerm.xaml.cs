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
    public partial class AddTerm : ContentPage
    {
        public AddTerm()
        {
            InitializeComponent();
        }

        

        async void Save_Clicked(object sender, EventArgs e)
        {
            int tossedInt;

            if (!Int32.TryParse(TermNumber.Text, out tossedInt))
            {
                await DisplayAlert("Missing Term number.", "Please enter a Term number (Numbers only).", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(TermTitle.Text))
            {
                await DisplayAlert("Missing Term Title/Name", "Please enter a Title/Name for the Term.", "OK");
                return;
            }

            if (TermStartDate.Date > TermEndDate.Date)
            {
                await DisplayAlert("End date is before Start date", "Please adjust dates so start date is before end date.", "OK");
                return;
            }

            await DatabaseService.AddTerm(Int32.Parse(TermNumber.Text), TermTitle.Text, DateTime.Parse(TermStartDate.Date.ToString()), DateTime.Parse(TermEndDate.Date.ToString()));

            await Navigation.PopAsync();  
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}