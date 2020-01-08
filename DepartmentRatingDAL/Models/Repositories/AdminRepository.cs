
using DepartmentRatingDAL.Models.DataBase;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentRatingDAL.Models.Repositories
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

        public void Delete(int id)
        {
            context.Admins.Remove(GetById(id));
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
