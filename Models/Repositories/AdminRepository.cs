using DepartmentRating.DataBase;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentRating.Repositories
{
    public class AdminRepository
    {
        CoreDbContext context;
        public AdminRepository(CoreDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Admin> GetAll()
        {
            return context.Admins;   
        }

        public Admin GetById(int id)
        {
            return context.Admins.Where(s => s.AdminId == id).FirstOrDefault<Admin>();
        }

        public int SaveAdmin(Admin entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();

            return entity.AdminId;
        }

        public void Delete(Admin entity)
        {
            context.Admins.Remove(entity);
            context.SaveChanges();
        }

        public bool IsUserAdmin(string account)
        {
            return true;
            if (account == null)
                return false;
            else
            {
                account = account.Split('\\')[1];

                return context.Admins.Any(s => s.Name == account);
            }
        }

    }
}
