using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using XavSpace.Website.Extensions;

namespace XavSpace.Website.Filters
{
    public class StaffOnlyAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext) && httpContext.User.Identity.IsStaff();
        }
    }
}