using System;
using System.Globalization;
using ClassTracker.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClassTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
	{
        public Term SelectedTerm { get; set; }
        public TermPage(Term term)
        {
            PublicClass.initializeCoursesCollection(term.Id);
            InitializeComponent();
            SelectedTerm = term;
            SetData(term);

        }

        public void SetData(Term term)
        {
            PageTitle.Text = term.Title;
            TermDateRange.Text = $"{term.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {term.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (PublicClass.Courses.Count >= 6)
                {
                    throw new Exception("You cannot add more than 6 courses to a term");
                }

                await Navigation.PushModalAsync(new CourseFormPage(SelectedTerm.Id));
                
            }
            catch (Exception error)
            {
                 await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermFormPage(this));
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            PublicClass.deleteTermFromTermCollection(SelectedTerm);
            await Navigation.PopAsync();
        }

        private async void Course_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var course = (Course)layout.BindingContext;
            await Navigation.PushAsync(new CoursePage(course));
        }
    }
}

