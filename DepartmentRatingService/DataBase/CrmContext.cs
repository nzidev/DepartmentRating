using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentRatingService.DataBase
{
    public class CrmContext : DbContext
    {
        public CrmContext() : base("CrmRatingConnectionString")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }

    }
}
