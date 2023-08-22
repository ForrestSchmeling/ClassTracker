using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ClassTracker.Models;

namespace ClassTracker.Views
{	
	public partial class AssessmentListPage : ContentPage
	{
        public Course SelectedCourse { get; set; }
        public AssessmentListPage(Course course)
        {
            PublicClass.initializeAssessmentCollection(course.Id);
            InitializeComponent();
            SelectedCourse = course;
            navTitle.Text = $"{course.Title} Assessments";
        }

        private async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (PublicClass.Assessments.Count >= 2)
                {
                    throw new Exception("You cannot add more than 2 assessments to a course");
                }
                await Navigation.PushModalAsync(new AssessmentFormPage(SelectedCourse.Id));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void Assessment_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var assessment = (Assessment)layout.BindingContext;
            await Navigation.PushAsync(new AssessmentPage(assessment));
        }
    }
}

