using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using AaronMorris_C971_Task1.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AaronMorris_C971_Task1.Services
{
    public static class DatabaseService
    {
        static SQLiteAsyncConnection _db;
        static async Task Init()
        {

            if (_db != null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "DatabaseForMobileApps.db");

            _db = new SQLiteAsyncConnection(databasePath);
  

            await _db.CreateTableAsync<Term>();
            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Assessment>();

            var terms = await DatabaseService.GetTerms();

            bool _971exists = false;
          

            foreach (Term term in terms)
            {
                if (term.termNumber == 1)
                {
                    _971exists = true;
                }
               
            }
            if (_971exists == false)
            {
                await LoadOnFirstRun();
            }
        }

        public static async Task LoadOnFirstRun()
        {
            var term1 = new Term()
            {
                termNumber = 1,
                termTitle = "Term 1",
                startDate = DateTime.Parse("7/1/2023"),
                endDate = DateTime.Parse("12/30/2023")
            };

            await _db.InsertAsync(term1);

            var myCourse = new Course()
            {
                CourseID = 971,
                TermNumber = 1,
                Name = "Mobile Application Development Using C#",
                StartDate = DateTime.Parse("01-11-2022"),
                EndDate = DateTime.Parse("01-05-2023"),
                CourseStatus = "In Progress",
                CourseInstructor = "Aaron Morris",
                CourseInstructorPhone = "7725325581",
                CourseInstructorEmail = "amor414@wgu.edu",
                CourseDetails = "No details",
                CourseNotes = "No notes"
            };

            await _db.InsertAsync(myCourse);

            var myAssessment1 = new Assessment()
            {
                assessName = "Mobile App C# Multiple Choice",
                assessType = "Objective",
                assessCourseId = 971,
                assessStartDate = DateTime.Parse("01-11-2022"),
                assessEndDate = DateTime.Parse("01-05-2023")
            };

            await _db.InsertAsync(myAssessment1);

            var myAssessment2 = new Assessment()
            {

                assessName = "Performance Assessment:Mobile Application Development Using C# (LAP1)",
                assessType = "Performance",
                assessCourseId = 971,
                assessStartDate = DateTime.Parse("01-11-2022"),
                assessEndDate = DateTime.Parse("01-05-2023")
            };

            await _db.InsertAsync(myAssessment2);

        }

        #region Course methods
        public static async Task AddCourse(int courseID, int termNumber,
            string name, DateTime startDate, DateTime endDate, string courseStatus,
            string courseInstructor, string courseInstructorPhone,
            string courseInstructorEmail, string courseDetails, string courseNotes)
        {
            await Init();
            var course = new Course()
            {
                CourseID = courseID,
                TermNumber = termNumber,
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                CourseStatus = courseStatus,
                CourseInstructor = courseInstructor,
                CourseInstructorPhone = courseInstructorPhone,
                CourseInstructorEmail = courseInstructorEmail,
                CourseDetails = courseDetails,
                CourseNotes = courseNotes
            };

            await _db.InsertAsync(course);

        }

        public static async Task RemoveCourse(int Id)
        {
            await Init();

            await _db.DeleteAsync<Course>(Id);
        }

        public static async Task<IEnumerable<Course>> GetCourses(int termNumber)
        {
            await Init();

            var courses = await _db.Table<Course>().Where(i => i.TermNumber == termNumber).ToListAsync();
            return courses;

        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await _db.Table<Course>().ToListAsync();
            return courses;

        }

        public static async Task UpdateCourse(int id,int courseID, int termNumber,
    string name, DateTime startDate, DateTime endDate, string courseStatus,
    string courseInstructor, string courseInstructorPhone,
    string courseInstructorEmail, string courseDetails, string courseNotes)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.CourseID = courseID;
                courseQuery.TermNumber = termNumber;
                courseQuery.Name = name;
                courseQuery.StartDate = startDate;
                courseQuery.EndDate = endDate;
                courseQuery.CourseStatus = courseStatus;
                courseQuery.CourseInstructor = courseInstructor;
                courseQuery.CourseInstructorPhone = courseInstructorPhone;
                courseQuery.CourseInstructorEmail = courseInstructorEmail;
                courseQuery.CourseDetails = courseDetails;
                courseQuery.CourseNotes = courseNotes;

                await _db.UpdateAsync(courseQuery);
            }
        }

        //counts the number of courses in a term
        public static async Task<int> GetCourseCountAsync(int selectedtermNumber)
        {
            int courseCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Course where TermNumber = ?", selectedtermNumber);
            return courseCount;
        }

        #endregion

        #region Term methods

        public static async Task AddTerm(int TermNumber, string TermName,
           DateTime StartDate, DateTime EndDate)
        {
            await Init();
            var term = new Term()
            {
                termNumber = TermNumber,
                termTitle = TermName,
                startDate = StartDate,
                endDate = EndDate,
                
            };

            await _db.InsertAsync(term);

        }

        public static async Task RemoveTerm(int Id)
        {
            await Init();

            await _db.DeleteAsync<Term>(Id);
        }

        public static async Task<IEnumerable<Term>> GetTerms()
        {
            await Init();

            var terms = await _db.Table<Term>().ToListAsync();
            return terms;

        }

        public static async Task UpdateTerms(int id, int TermNumber, string TermName,
           DateTime StartDate, DateTime EndDate)
        {
            await Init();

            var termQuery = await _db.Table<Term>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (termQuery != null)
            {
               termQuery.termNumber = TermNumber;
               termQuery.termTitle = TermName;
               termQuery.startDate = StartDate;
               termQuery.endDate = EndDate;

                await _db.UpdateAsync(termQuery);
            }
        }


        #endregion

        #region Assessment methods

        public static async Task AddAssessment(int courseID,
          string type, string name, DateTime startDate, DateTime endDate)
        {
            await Init();
            var assignment = new Assessment()
            {
                assessCourseId = courseID,
                assessType = type,
                assessName = name,
                assessStartDate = startDate,
                assessEndDate = endDate
            };

            await _db.InsertAsync(assignment);

        }

        public static async Task RemoveAssessment(int Id)
        {
            await Init();

            await _db.DeleteAsync<Assessment>(Id);
        }

        public static async Task<IEnumerable<Assessment>> GetAssessments(int assessCourseId)
        {
            await Init();

            var assessments = await _db.Table<Assessment>().Where(i => i.assessCourseId == assessCourseId).ToListAsync();
            return assessments;

        }
        public static async Task<IEnumerable<Assessment>> GetAssessments()
        {
            await Init();

            var assessments = await _db.Table<Assessment>().ToListAsync();
            return assessments;

        }

        public static async Task UpdateAssessment(int id, int courseID,
          string type, string name, DateTime startDate, DateTime endDate)
        {
            await Init();

            var assessQuery = await _db.Table<Assessment>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (assessQuery != null)
            {
                assessQuery.assessCourseId = courseID;
                assessQuery.assessType = type;
                assessQuery.assessName = name;
                assessQuery.assessStartDate = startDate;
                assessQuery.assessEndDate = endDate;
                

                await _db.UpdateAsync(assessQuery);
            }
        }

        //counts the number of assessments in a course
        public static async Task<int> GetAssessCountAsync(int selectedCourseId)
        {
            int courseCount = await _db.ExecuteScalarAsync<int>($"Select Count(*) from Course where assessCourseId = ?", selectedCourseId);
            return courseCount;
        }


        #endregion


        public static async Task ClearDatabaseData()
        {
            await Init();

            await _db.DropTableAsync<Course>();
            await _db.DropTableAsync<Term>();
            await _db.DropTableAsync<Assessment>();
            _db = null;
        
        }
    }
}
