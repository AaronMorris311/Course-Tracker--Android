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
    public partial class EditAssessment : ContentPage
    {
        int assessId;
        int currentCourseId;

        public EditAssessment(Assessment assessment)
        {
            InitializeComponent();

            assessId = assessment.Id;
            currentCourseId = assessment.assessCourseId;

            AssessName.Text = assessment.assessName.ToString();
            AssessStartDate.Date = assessment.assessStartDate;
            AssessEndDate.Date = assessment.assessEndDate;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var assessments = await DatabaseService.GetAssessments(currentCourseId);

            bool ObjExist = false;
            bool PerExist = false;

            foreach (Assessment assessment in assessments)
            {
                if (assessment.assessType == "Objective" && assessment.Id != assessId)
                {
                    ObjExist = true;
                }
                if (assessment.assessType == "Performance" && assessment.Id != assessId)
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

            await DatabaseService.UpdateAssessment(assessId, currentCourseId, AssessType.SelectedItem.ToString(), AssessName.Text,
                DateTime.Parse(AssessStartDate.Date.ToString()), DateTime.Parse(AssessEndDate.Date.ToString()));

            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete this assessment?", "Delete this assessment?", "Yes", "No");

            if (answer == true)
            {

                var id = assessId;

                await DatabaseService.RemoveAssessment(id);

                await DisplayAlert("Assessment has been deleted.", "Assessment has been deleted.", "OK");

            }
            else
            {
                await DisplayAlert("Delete cancelled.", "Assessment was not deleted.", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}
