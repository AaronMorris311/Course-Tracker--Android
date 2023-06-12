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
    public partial class AssessmentsPage : ContentPage
    {
        int courseId;

        public AssessmentsPage(int CourseID)
        {
            InitializeComponent();

          courseId = CourseID;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AssignmentCollectionView.ItemsSource = await DatabaseService.GetAssessments(courseId);
        }


        async void AssignmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if (e.CurrentSelection != null)
                {
                    Assessment assessment = (Assessment)e.CurrentSelection.FirstOrDefault();
                    await Navigation.PushAsync(new EditAssessment(assessment));
                }
            
        }

        async void AddAssignment_Clicked(object sender, EventArgs e)
        {
            {
                await Navigation.PushAsync(new AddAssessment(courseId));
            }
        }
    }
}