using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using XavSpace.Entities.Relationships;

namespace XavSpace.Entities.Users
{
    /// <summary>
    /// Represents a generic user on the system
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// The name of the user
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// The UId provided by the organization
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// The Gender of the user
        /// </summary>
        public Gender Gender { get; set; }

        public ApplicationUser()
        {
            Name = new Name();
            Gender = Gender.Male;
        }

        public virtual List<UserNoticePost> Posts { get; set; }
    }
}
