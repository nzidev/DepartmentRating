
using DepartmentRatingDAL.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentRatingDAL.Models.Repositories
{
    public class MainRepository : IRepository<Main>
    {
        CoreDbContext context;

        public MainRepository(CoreDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<Main> GetAll()
        {
            return context.Mains;
        }

        public Main GetById(int id)
        {
            return context.Mains.Where(s => s.MainId == id).FirstOrDefault<Main>();
        }
        public void Delete(Main entity)
        {
            context.Mains.Remove(entity);
            context.SaveChanges();
        }

        public int SaveMain(Main entity)
        {
            if (entity.MainId == default)
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            else
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return entity.OffenceId;
        }

        /// <summary>
        /// Удалить все события
        /// </summary>
        public void DeleteEverything()
        {
            var queryMain = context.Mains.Where(s => s.MainId > 0).ToList();
            context.Mains.RemoveRange(queryMain);
            context.SaveChanges();
        }
    }
}
