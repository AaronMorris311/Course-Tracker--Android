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
    public partial class AddAssessment : ContentPage
    {
        int courseId;
        public AddAssessment(int CourseID)
        {
            InitializeComponent();

            courseId = CourseID;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var assessments = await DatabaseService.GetAssessments(courseId);

            bool ObjExist = false;
            bool PerExist = false;

            foreach (Assessment assessment in assessments)
            {
                if (assessment.assessType == "Objective")
                {
                    ObjExist = true;
                }
                if (assessment.assessType == "Performance")
                {
                    PerExist = true;
                }
            }

            if (ObjExist && PerExist)
            {
                await DisplayAlert("Both an Objective and Performance Assessment Already Exists", "You cannot add another assessment.", "OK");
                return;
            }

            if ((AssessType.SelectedItem.ToString() == "Objective") && ObjExist)
            {
                await DisplayAlert("An Objective Assessment Already Exists", "You cannot add another Objective assessment.", "OK");
                return;
            }

            if ((AssessType.SelectedItem.ToString() == "Performance") && PerExist)
            {
                await DisplayAlert("An Performance Assessment Already Exists", "You cannot add another Performance assessment.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(AssessName.Text))
            {
                await DisplayAlert("Missing Assessment Title/Name", "Please enter a Title/Name for the Assessment.", "OK");
                return;
            }

            if (AssessType.SelectedIndex == -1)
            {
                await DisplayAlert("Missing Assessment Type", "Please choose a type.", "OK");
                return;
            }

            if (AssessStartDate.Date > AssessEndDate.Date)
            {
                await DisplayAlert("End date is before Start date", "Please adjust dates so start date is before end date.", "OK");
                return;
            }



            await DatabaseService.AddAssessment(courseId, AssessType.SelectedItem.ToString(), AssessName.Text,
                DateTime.Parse(AssessStartDate.Date.ToString()), DateTime.Parse(AssessEndDate.Date.ToString()));

            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
