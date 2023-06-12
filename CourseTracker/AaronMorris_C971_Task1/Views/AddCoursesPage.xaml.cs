using AaronMorris_C971_Task1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace AaronMorris_C971_Task1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCoursesPage : ContentPage
    {
        int _selectedTermNumber;

        public AddCoursesPage(int termNumber)
        {
            InitializeComponent();
            _selectedTermNumber = termNumber;

            TermNumber.Text = _selectedTermNumber.ToString();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            int tossedInt;

            if (!Int32.TryParse(CourseID.Text, out tossedInt))
            {
                await DisplayAlert("Missing Course ID#", "Please enter a Course ID number (Numbers only).", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                await DisplayAlert("Missing Assessment Title/Name", "Please enter a Title/Name for the Course.", "OK");
                return;
            }

            if (StartDate.Date > EndDate.Date)
            {
                await DisplayAlert("End date is before Start date", "Please adjust dates so start date is before end date.", "OK");
                return;
            }

            if (CourseStatus.SelectedIndex == -1)
            {
                await DisplayAlert("Missing Course Status", "Please enter a status.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseInstructor.Text))
            {
                await DisplayAlert("Missing Course Instructor Name", "Please enter Course Instructor's Name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseInstructorPhone.Text))
            {
                await DisplayAlert("Missing Course Instructor Phone Number", "Please enter Course Instructor's Phone Number.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseInstructorEmail.Text))
            {
                await DisplayAlert("Missing Course Instructor Email", "Please enter Course Instructor's Email.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseDetails.Text))
            {
                await DisplayAlert("Missing Course Details", "Please enter Course Details.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseNotes.Text))
            {
                await DisplayAlert("Missing Course Notes", "Please enter Course Notes.", "OK");
                return;
            }

            await DatabaseService.AddCourse(Int32.Parse(CourseID.Text), _selectedTermNumber,
             CourseName.Text, DateTime.Parse(StartDate.Date.ToString()), DateTime.Parse(EndDate.Date.ToString()),
             CourseStatus.SelectedItem.ToString(), CourseInstructor.Text, CourseInstructorPhone.Text,
             CourseInstructorEmail.Text, CourseDetails.Text, CourseNotes.Text);

            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}