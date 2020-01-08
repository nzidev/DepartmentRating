using DepartmentRatingDAL.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentRatingDAL.Models.Repositories
{
    public class EventLogRepository
    {
        CoreDbContext context;
        public EventLogRepository(CoreDbContext context)
        {
            this.context = context;
        }
        public int SaveEvent(EventLog entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
            return entity.EventLogId;
        }
    }
}
