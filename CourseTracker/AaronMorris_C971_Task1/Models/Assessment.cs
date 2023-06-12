using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AaronMorris_C971_Task1.Models
{
   public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int assessCourseId { get; set; }
        public string assessType { get; set; }
        public string assessName { get; set; }
        public DateTime assessStartDate { get; set; }
        public DateTime assessEndDate { get; set; }
    }
}
