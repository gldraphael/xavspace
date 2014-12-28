using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using XavSpace.Entities.Users;
using XavSpace.Facade.Identity.Managers;

namespace XavSpace.Website.Extensions
{
    public static class IdentityApplicationUserExtension
    {
        public static async Task<ApplicationUser> GetApplicationUserAsync(this IIdentity identity)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return await userManager.FindByIdAsync(identity.GetUserId());
        }
        public static ApplicationUser GetApplicationUser(this IIdentity identity)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return userManager.FindById<ApplicationUser, string>(identity.GetUserId());
        }
        public static ApplicationUser GetApplicationUser(this IIdentity identity, string id)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return userManager.FindById<ApplicationUser, string>(id);
        }
        public static bool HasPassword(this IIdentity identity)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        #region Check User Type
        public static bool IsStaff(this IIdentity identity)
        {
            return !(identity.GetApplicationUser() as Staff == null);
        }

        public static async Task<bool> IsStaffAsync(this IIdentity identity)
        {
            return !(await identity.GetApplicationUserAsync() as Staff == null);
        }

        public static bool IsModerator(this IIdentity identity)
        {
            return !(identity.GetApplicationUser() as Moderator == null);
        }

        public static async Task<bool> IsModeratorAsync(this IIdentity identity)
        {
            return !(await identity.GetApplicationUserAsync() as Moderator == null);
        }
        #endregion

    }
}