using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLitePCL;

namespace AaronMorris_C971_Task1.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int termNumber { get; set; }
        public string termTitle { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
