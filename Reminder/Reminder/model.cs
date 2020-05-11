using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder
{
    public class model
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Text { get; set; } = "";
        public string DateTime { get { return Date + " " + Time; } }
        public int flag { get; set; } = 1;
    }
}
