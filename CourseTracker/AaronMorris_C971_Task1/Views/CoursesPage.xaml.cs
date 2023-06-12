using AaronMorris_C971_Task1.Models;
using AaronMorris_C971_Task1.Services;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace AaronMorris_C971_Task1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursesPage : ContentPage
    {
        int currentTermNumber;

        public CoursesPage(int termNumber)
        {
            this.BindingContext = this;
            Title = "Term " + termNumber;
            InitializeComponent();
            currentTermNumber = termNumber;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await DatabaseService.GetCourses(currentTermNumber);
            
           
        }

        async void AddCourse_Clicked(object sender, EventArgs e)
        {
            int tempCount;
            tempCount = await DatabaseService.GetCourseCountAsync(currentTermNumber);
            
            if(tempCount > 5)
            {
                await DisplayAlert("Cannot add another course to this term.", "Please delete a course before trying to add a new one.", "OK");
                return;
            }
            await Navigation.PushAsync(new AddCoursesPage(currentTermNumber));
        }

        async void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Course course = (Course)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new EditCoursesPage(course));
            }
        }

        async void EditTerm_Clicked(object sender, EventArgs e)
        {
           var Terms = await DatabaseService.GetTerms();

            var term = Terms.FirstOrDefault(i => i.termNumber == currentTermNumber);

            await Navigation.PushAsync(new EditTerm(term));
        }

        async void Assessments_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var course = (Course)btn.BindingContext;

            await Navigation.PushAsync(new AssessmentsPage(course.CourseID));
        }

        async void ShareNotes_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var course = (Course)btn.BindingContext;
            var text = course.CourseNotes;

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Notes"
            });
        }
    }
}