﻿using System;
using SQLite;
using System.Collections.Generic;
using System.IO;
using ClassTracker.Models;


namespace ClassTracker.Database
{
	public class SQLDatabase : ILocalDataService
    {
        private SQLiteConnection database;
        public bool Initialize()
        {
            bool dbTablesCreated = false;
            if (database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TermTrackerDb.db3");
                database = new SQLiteConnection(dbPath);
            }

            var create = database.CreateTables<Term, Course, Assessment>();

            foreach (var result in create.Results)
            {
                dbTablesCreated = dbTablesCreated || result.Value == CreateTableResult.Created;
            }

            return dbTablesCreated;
        }

        public void AddTerm(Term term)
        {
            database.Insert(term);
        }

        public List<Term> GetTerms()
        {
            return database.Table<Term>().ToList();
        }

        public List<Course> GetCourses()
        {
            return database.Table<Course>().ToList();
        }

        public List<Assessment> GetAssessments()
        {
            return database.Table<Assessment>().ToList();
        }

        public int UpdateTerm(Term term)
        {
            return database.Update(term);
        }

        public int DeleteTerm(Term term)
        {
            return database.Delete(term);
        }

        public void AddCourse(Course course)
        {
            database.Insert(course);
        }

        public List<Course> GetCoursesById(int termId)
        {
            string query = $"SELECT * FROM course WHERE course.TermId={termId}";
            return database.Query<Course>(query);
        }

        public void Close()
        {
            database.Close();
        }

        public int UpdateCourse(Course course)
        {
            return database.Update(course);
        }

        public int DeleteCourse(Course course)
        {
            return database.Delete(course);
        }

        public void AddAssessment(Assessment assessment)
        {
            database.Insert(assessment);
        }

        public List<Assessment> GetAssessmentsById(int courseId)
        {
            string query = $"SELECT * FROM assessment WHERE assessment.CourseId={courseId}";
            return database.Query<Assessment>(query);
        }

        public int UpdateAssessment(Assessment assessment)
        {
            return database.Update(assessment);
        }

        public int DeleteAssessment(Assessment assessment)
        {
            return database.Delete(assessment);
        }

        public List<Term> GetAllTerms()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public List<Assessment> GetAllAssessments()
        {
            throw new NotImplementedException();
        }

        public List<Course> GetCoursesByTermId(int termId)
        {
            throw new NotImplementedException();
        }

        public List<Assessment> GetAssessmentsByCourseId(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}

