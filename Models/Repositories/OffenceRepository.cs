using DepartmentRating.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace DepartmentRating.Models.Repositories
{
    public class OffenceRepository : IRepository<Offence>
    {
        CoreDbContext context;

        public OffenceRepository(CoreDbContext coreDbContext)
        {
            this.context = coreDbContext;
        }

        public IEnumerable<Offence> GetAll()
        {
            return context.Offences;
        }

        public Offence GetById(int id)
        {
            return context.Offences.Where(s => s.OffenceId == id).FirstOrDefault<Offence>();
        }

        public int SaveOffence(Offence entity)
        {
            if(entity.OffenceId == default)
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            else
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return entity.OffenceId;
        }

        public void Delete(Offence entity)
        {
            context.Offences.Remove(entity);
            context.SaveChanges();
        }
    }
}
