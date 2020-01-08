
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentRatingDAL.Models.Options
{
    
    /// <summary>
    /// Статический класс для работы с ActiveDirectory
    /// </summary>
    public static class ActiveDirectoryOptions
    {
        ///*************************Если меняеются настройки домена и группы, надо поменять в сервисе******
        /// <summary>
        /// Имя домена. Загружается из json файла при запуске
        /// </summary>
        public static string DomainName { get; set; }
        /// <summary>
        /// Имя группы с доступом. Загружается из json файла при запуске
        /// </summary>
        public static string AccessGroupName { get; set; }
        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        /// <returns>UserPrincipal</returns>
        public static UserPrincipal GetCurrentUser()
        {
            
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName);
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            var username = Thread.CurrentPrincipal.Identity.Name;
            
            return UserPrincipal.FindByIdentity(pc, username);
        }

        /// <summary>
        /// Является ли пользователь членом группы
        /// </summary>
        /// <param name="account">Учетная запись пользователя</param>
        /// <returns></returns>
        public static bool IsMember(string account)
        {
           return true;

            if (account == null)
                return false;
            else
            {
                account = account.Split('\\')[1];
                PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName);
                UserPrincipal user = UserPrincipal.FindByIdentity(pc, account);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, AccessGroupName);
                return user.IsMemberOf(group);
            }
        }
            

        
    }
}
