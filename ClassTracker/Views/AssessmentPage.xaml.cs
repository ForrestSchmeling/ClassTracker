using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ClassTracker.Models;
using System.Globalization;

namespace ClassTracker.Views
{	
	public partial class AssessmentPage : ContentPage
	{
        private Assessment SelectedAssessment;
        public AssessmentPage(Assessment assessment)
        {
            InitializeComponent();
            SelectedAssessment = assessment;
            SetData(assessment);
        }

        public void SetData(Assessment assessment)
        {
            PageTitle.Text = assessment.Title;
            AssessmentDateRange.Text = $"{assessment.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {assessment.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            TypeSelection.Text = assessment.Type;
            notificationsEnabled.Text = assessment.EnableNotifications ? "ON" : "OFF";
        }

        private async void EditAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AssessmentFormPage(this, SelectedAssessment));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            PublicClass.deleteAssessmentFromAssessmentCollection(SelectedAssessment);
            await Navigation.PopAsync();
        }
    }
}

