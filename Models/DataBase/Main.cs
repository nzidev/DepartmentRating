using System;


namespace DepartmentRating.DataBase
{
    public class Main
    {
        public int MainId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int OffenceId { get; set; }
        public virtual Offence Offence { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
        public string Owner { get; set; }

    }
}
