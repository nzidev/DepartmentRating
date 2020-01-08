using DepartmentRating.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentRating.Models.Repositories
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        void Delete(T entity);
    }
}
