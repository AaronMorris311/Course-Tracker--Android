using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using SQLitePCL;

namespace AaronMorris_C971_Task1.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseID { get; set; }
        public int TermNumber { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseStatus { get; set; }
        public string CourseInstructor { get; set; }
        public string CourseInstructorPhone { get; set; }
        public string CourseInstructorEmail { get; set; }
        public string CourseDetails { get; set; }
        public string CourseNotes { get; set; }
      
    }
}
