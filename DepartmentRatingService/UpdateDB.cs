using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentRatingService.DataBase;

namespace DepartmentRatingService
{
    public class UpdateDB
    {
        public delegate void DbHandler(string message);
        public event DbHandler Notify;
        //Если меняется тут, надо поменять и в DepartmentRating\appsettings.json
        public const string DomainName = "dpc.tax.nalog.ru";
        public const string AccessGroupName = "n7701-ETI#3";

        public void Start()
        {
            Notify?.Invoke("Запуск UpdateDB");

            AddUserToDB();
            Notify?.Invoke("Закончили UpdateDB");
        }

        public void Update()
        {
            AddUserToDB();
        }

        public void AddUserToDB()
        {
            string GroupName = AccessGroupName;
            //Notify?.Invoke("Запуск AddUserToDB");
            using (var CrmContext = new CrmContext())
            {
                using (var context = new PrincipalContext(ContextType.Domain, DomainName))
                {
                    using (var group = GroupPrincipal.FindByIdentity(context, GroupName))
                    {
                        if (group != null)
                        {
                            var users = group.GetMembers(true);
                            //Удаляем всех кто не входит в группу
                            var excludedIDs = new HashSet<string>(users.Select(p => p.SamAccountName));
                            var result = CrmContext.Users.Where(p => !excludedIDs.Contains(p.Account));
                            foreach (var remUser in result)
                            {
                                CrmContext.Users.Remove(remUser);

                                Notify?.Invoke(String.Format("Удаляем {0}, т.к. данной УЗ нет в группе {1}", remUser.Account.ToString(), AccessGroupName));
                               // EventToDb(CrmContext, String.Format("Удаляем {0}, т.к. данной УЗ нет в группе {1}", remUser.Account.ToString(), AccessGroupName));
                            }

                            foreach (UserPrincipal u in users)
                            {
                                if (u.Enabled==true)
                                {
                                    var query = CrmContext.Users.Where(s => s.Account == u.SamAccountName).FirstOrDefault<User>();

                                    if (query == null)
                                    {

                                        User user = new User()
                                        {
                                            Account = u.SamAccountName,
                                            Fio = u.DisplayName,
                                            Rating = 100,
                                            Enabled = true
                                        };
                                        CrmContext.Users.Add(user);
                                        CrmContext.SaveChanges();


                                        Notify?.Invoke("Добавлен новый пользователь " + user.Account);
                                        //EventToDb(CrmContext, "Добавлен новый пользователь " + user.Account);

                                    }
                                    else
                                    {
                                        query.Fio = u.DisplayName;
                                    }
                                }
                                //Если УЗ отключили, отключаем в базе
                                else if(CrmContext.Users.Where(s => s.Account == u.SamAccountName).FirstOrDefault<User>().Enabled==true)
                                {
                                    var query = CrmContext.Users.Where(s => s.Account == u.SamAccountName).FirstOrDefault<User>();
                                    query.Enabled = false;
                                    
                                    //EventToDb(CrmContext, "Пользователь " + query.Account + "отключен");
                                    Notify?.Invoke("Пользователь " + query.Account + " отключен");
                                    Notify?.Invoke("Сохраняем CrmContext");
                                    try { 
                                    CrmContext.SaveChanges();
                                    }
                                    catch(Exception ex)
                                    {
                                        Notify?.Invoke(ex.Message + " " + DateTime.Now);
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            Notify?.Invoke("Группа пустая");
                        }
                    }
                }

            }
        }

        private void EventToDb(CrmContext crmContext, string message)
        {
            Notify?.Invoke("Добавляем в лог");
            EventLog eventLog = new EventLog()
            {
                Date = DateTime.Now,
                Description = message,
                Owner = "Service",
                Table = "User"
            };
            crmContext.Entry(eventLog).State = EntityState.Added;
            Notify?.Invoke("Добавляем в CrmContext");
            crmContext.EventLogs.Add(eventLog);
           
            
        }
    }
}
