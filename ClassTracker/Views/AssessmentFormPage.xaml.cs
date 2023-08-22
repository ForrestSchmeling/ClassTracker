using System;
using System.Collections.Generic;
using System.Linq;
using ClassTracker.Models;

using Xamarin.Forms;

namespace ClassTracker.Views
{	
	public partial class AssessmentFormPage : ContentPage
	{
        private int CourseID;
        private Assessment SelectedAssessment;
        private AssessmentPage AssessmentPageRef;

        public AssessmentFormPage(int courseId)
        {
            InitializeComponent();
            CourseID = courseId;
            SaveEditButton.IsVisible = false;
        }

        public AssessmentFormPage(AssessmentPage assessmentPage, Assessment assessment)
        {
            InitializeComponent();
            CourseID = assessment.CourseId;
            AssessmentPageRef = assessmentPage;
            SelectedAssessment = assessment;
            assessmentTitle.Text = assessment.Title;
            startDateSelected.Date = assessment.StartDate;
            endDateSelected.Date = assessment.EndDate;
            typePicker.SelectedItem = assessment.Type;
            notificationSwitch.IsToggled = assessment.EnableNotifications;
            SaveButton.IsVisible = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (assessmentTitle.Text == null || assessmentTitle.Text == "")
                {
                    throw new Exception("You must have a Assessment Title");
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception("The start date cannot be after the end date");
                }

                if (typePicker.SelectedItem == null)
                {
                    throw new Exception("You must pick a Assessment Type");
                }

                if (PublicClass.Assessments.Any())
                {
                    if (PublicClass.Assessments.First().Type == typePicker.SelectedItem.ToString())
                    {
                        throw new Exception("You cannot have two assessments of the same type");
                    }
                }

                var newAssessment = new Assessment
                {
                    CourseId = CourseID,
                    Title = assessmentTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Type = typePicker.SelectedItem.ToString(),
                    EnableNotifications = notificationSwitch.IsToggled
                };
                PublicClass.addAssessmentToAssessmentCollection(newAssessment);
                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void SaveEditButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (assessmentTitle.Text == "")
                {
                    throw new Exception("You must have a Assessment Title");
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception("The start date cannot be after the end date");
                }

                if (PublicClass.Assessments.Where(test => test.Id != SelectedAssessment.Id).First().Type == typePicker.SelectedItem.ToString())
                {
                    throw new Exception("You cannot have two assessments of the same type");
                }

                var newAssessment = new Assessment
                {
                    Id = SelectedAssessment.Id,
                    CourseId = CourseID,
                    Title = assessmentTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Type = typePicker.SelectedItem.ToString(),
                    EnableNotifications = notificationSwitch.IsToggled
                };
                PublicClass.updateAssessmentInAssessmentCollection(SelectedAssessment, newAssessment);
                AssessmentPageRef.SetData(newAssessment);
                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

