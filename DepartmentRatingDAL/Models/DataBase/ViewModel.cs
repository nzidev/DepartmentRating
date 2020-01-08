using DepartmentRatingDAL.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DepartmentRatingDAL.Models.DataBase
{
    

    public class ViewModel
    {
        public delegate void DbHandler(string message);
        public event DbHandler Notify;

        public bool isAdmin;
        /// <summary>
        /// Таблица админиистраторов
        /// </summary>
        public IEnumerable<Admin> Admins { get; set; }
        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public IEnumerable<User> Users { get; set; }
        /// <summary>
        /// Таблица возможных событий
        /// </summary>
        public IEnumerable<Offence> Offences { get; set; }
        /// <summary>
        /// Основная таблица с событиями
        /// </summary>
        public IEnumerable<Main> Mains { get; set; }
        /// <summary>
        /// Таблица журнала действий
        /// </summary>
        public IEnumerable<EventLog> EventLogs { get; set; }
        /// <summary>
        /// Является ли пользователь администратором
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            //return true;
            return isAdmin;
        }

    }
}
