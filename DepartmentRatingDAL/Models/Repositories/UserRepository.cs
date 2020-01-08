using DepartmentRatingDAL.Models.DataBase;
using System.Collections.Generic;
using System.Linq;


namespace DepartmentRatingDAL.Models.Repositories
{
    public class UserRepository
    {
        CoreDbContext context;
        public UserRepository(CoreDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public User GetById(int id)
        {
            return context.Users.Where(s => s.UserId == id).FirstOrDefault<User>();
        }

        /// <summary>
        /// Изменяем рейтинг пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="offence">Событие</param>
        public void ChangeRating(User user, Offence offence)
        {
            if(user.Rating - offence.Cost < 0)
            {
                user.Rating = 0;
            }
            else
            {
                user.Rating = user.Rating - offence.Cost;
            }
        }

        /// <summary>
        /// Сброс всех рейтингов
        /// </summary>
        public void ResetEveryoneRating()
        {
            var query = context.Users.Where(s => s.Enabled == true).ToList();
            foreach (var user in query)
            {
                user.Rating = 100;
            }
            context.SaveChanges();
        }
    }
}
