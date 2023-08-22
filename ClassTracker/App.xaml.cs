using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClassTracker.Database;
using ClassTracker.Models;
using ClassTracker.Views;

namespace ClassTracker
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeData();
            InitializeComponent();
            PublicClass.startupNotifications();

            MainPage = new NavigationPage(new MainPage());
        }

        private void InitializeData()
        {
            var db = new SQLDatabase();
            bool addData = db.Initialize();

            if (addData)
            {
                var thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                db.AddTerm(new Term { Title = "Term 1", StartDate = thisMonth, EndDate = thisMonth.AddMonths(4).AddDays(-1) });
                db.AddCourse(new Course
                {
                    TermId = 1,
                    Title = "Test Course",
                    StartDate = thisMonth,
                    EndDate = thisMonth.AddMonths(1).AddDays(-1),
                    Status = "In Progress",
                    InstructorName = "Forrest Schmeling",
                    InstructorPhone = "253-258-9519", 
                    InstructorEmail = "Fschmel@wgu.edu",
                    Notes = "This is test data that can be entered as notes",
                    EnableNotifications = true
                });
                db.AddAssessment(new Assessment
                {
                    CourseId = 1,
                    Title = "Assessment 1",
                    StartDate = thisMonth.AddMonths(1).AddDays(-1),
                    EndDate = thisMonth.AddMonths(1),
                    Type = "Performance",
                    EnableNotifications = true
                });
                db.AddAssessment(new Assessment
                {
                    CourseId = 1,
                    Title = "Assessment 2",
                    StartDate = thisMonth.AddMonths(2).AddDays(-1),
                    EndDate = thisMonth.AddMonths(2),
                    Type = "Objective",
                    EnableNotifications = true
                });
            }
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

