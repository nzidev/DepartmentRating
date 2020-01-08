using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentRatingService.DataBase
{
    public class User
    {
        public int UserId { get; set; }
        public string Account { get; set; }
        public string Fio { get; set; }
        public int Rating { get; set; }
        public Boolean Enabled { get; set; }
        public override string ToString()
        {
            return "User";
        }
    }
}
