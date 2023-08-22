using System.Collections.Generic;
using ClassTracker.Models;


namespace ClassTracker.Database
{
    public interface ILocalDataService
    {
        bool Initialize();
        void Close();
        void AddTerm(Term term);
        List<Term> GetAllTerms();
        List<Course> GetAllCourses();
        List<Assessment> GetAllAssessments();
        int UpdateTerm(Term term);
        int DeleteTerm(Term term);
        void AddCourse(Course course);
        List<Course> GetCoursesByTermId(int termId);
        int UpdateCourse(Course course);
        int DeleteCourse(Course course);
        void AddAssessment(Assessment assessment);
        List<Assessment> GetAssessmentsByCourseId(int courseId);
        int UpdateAssessment(Assessment assessment);
        int DeleteAssessment(Assessment assessment);
    }
}