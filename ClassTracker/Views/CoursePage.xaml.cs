using System;
using System.Globalization;
using ClassTracker.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassTracker.Views
{	
	public partial class CoursePage : ContentPage
	{
        public Course SelectedCourse { get; set; }
        public CoursePage(Course course)
		{ 
			InitializeComponent ();
            SetData(course);
        }
        public void SetData(Course course)
        {
            SelectedCourse = course;
            PageTitle.Text = course.Title;
            CourseDateRange.Text = $"{course.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {course.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            statusSelection.Text = course.Status;
            instructorsName.Text = course.InstructorName;
            instructorsPhone.Text = course.InstructorPhone;
            instructorsEmail.Text = course.InstructorEmail;
            notes.Text = course.Notes;
            notificationsEnabled.Text = course.EnableNotifications ? "ON" : "OFF";
        }

        private async void ShareNotes_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync($"Notes from {PageTitle.Text}:\n{notes.Text}");
        }

        private async void CourseAssessments_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentListPage(SelectedCourse));
        }

        private async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CourseFormPage(this));
        }

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            PublicClass.deleteCourseFromCourseCollection(SelectedCourse);
            await Navigation.PopAsync();
        }
    }
}

