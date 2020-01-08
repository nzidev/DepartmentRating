using System;
using System.Collections.Generic;

namespace DepartmentRating.DataBase
{
    public class User
    {
        public int UserId { get; set; }
        public string Account { get; set; }
        public string Fio { get; set; }
        public int Rating { get; set; }
        public Boolean Enabled { get; set; }

        public virtual ICollection<Main> Main { get; set; }

        public override string ToString()
        {
            return "User";
        }
    }
}
