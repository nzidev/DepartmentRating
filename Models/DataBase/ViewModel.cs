using DepartmentRating.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DepartmentRating.DataBase
{
    public class ViewModel
    {
        public bool isAdmin;
        public IEnumerable<Admin> Admins { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Offence> Offences { get; set; }
        public IEnumerable<Main> Mains { get; set; }

        public bool IsAdmin()
        {
            //return true;
            return isAdmin;
        }

    }
}
