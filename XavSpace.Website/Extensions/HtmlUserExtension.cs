using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace XavSpace.Website.Extensions
{
    public static class HtmlUserExtension
    {
        /// <summary>
        /// Returns the current user's name
        /// Returns the email ID if name is not available
        /// </summary>
        public static string CurrentUserDisplayName(this HtmlHelper helper, IPrincipal principal)
        {
            var user = principal.Identity.GetApplicationUser();

            if(user.Name != null)
            {
                if (!String.IsNullOrWhiteSpace(user.Name.First) || !String.IsNullOrWhiteSpace(user.Name.Last))
                    return user.Name.ToString();
            }

            return user.Email;
        }
    }
}