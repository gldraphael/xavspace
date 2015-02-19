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
using XavSpace.Facade.Managers;
using XavSpace.Entities.Relationships;

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

        public static async Task<bool> IsSubscribedToAsync(this IIdentity identity, int noticeBoardId)
        {
            var id = identity.GetUserId();
            using (RelationshipManager rm = new RelationshipManager())
            {
                if (await rm.ExistsAsync(new UserNoticeBoardFollow { NoticeBoardId = noticeBoardId, UserId = id }))
                {
                    return true;
                }
                return false;
            }
        }

        #region Old Code
        //// Checks if the current user has a local password
        //public static bool HasPassword(this IIdentity identity)
        //{
        //    var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    var user = userManager.FindById(identity.GetUserId());
        //    if (user != null)
        //    {
        //        // check if the user has a password
        //        return user.PasswordHash != null;
        //    }

        //    // user doesn't exists
        //    return false;
        //} 
        #endregion

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