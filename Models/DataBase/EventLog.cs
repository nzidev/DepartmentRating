using System;


namespace DepartmentRating.DataBase
{
    public class EventLog
    {
        public int EventLogId { get; set; }
        public DateTime Date { get; set; }
        public string Table { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
    }
}
