
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentRating.Models.Options
{
    
    /// <summary>
    /// Статический класс для работы с ActiveDirectory
    /// </summary>
    public static class ActiveDirectoryOptions
    {

        public static string DomainName { get; set; }
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

        public static bool IsMember(string account)
        {
          //  return true;

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
