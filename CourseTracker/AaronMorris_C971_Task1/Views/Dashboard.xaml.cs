using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using AaronMorris_C971_Task1.Services;
using AaronMorris_C971_Task1.Models;

namespace AaronMorris_C971_Task1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var courses = await DatabaseService.GetCourses();
            var assessments = await DatabaseService.GetAssessments();
            //var notifyId = notifyRandom.Next(1000);

            foreach (Course course in courses)
            {
                if(course.StartDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Notice", $"{course.Name} begins today!");//, notifyId;
                }
                if (course.EndDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Notice", $"{course.Name} ends today!");//, notifyId;
                }
            }
            foreach (Assessment assessment in assessments)
            {
                if (assessment.assessStartDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Notice", $"{assessment.assessName} begins today!");//, notifyId;
                }
                if (assessment.assessEndDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Notice", $"{assessment.assessName} ends today!");//, notifyId;
                }
            }

        }

        async void Terms_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermsPage());
        }

        async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }
    }
}