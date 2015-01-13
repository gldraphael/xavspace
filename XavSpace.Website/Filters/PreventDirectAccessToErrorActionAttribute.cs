using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XavSpace.Website.Filters
{
    class PreventDirectAccessToErrorActionAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool? v = filterContext.RouteData.Values["fromAppErrorEvent"] as bool?;
            if (!v.HasValue)
            {
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.Result = new ViewResult { ViewName = "Error" };
            }
        }
    }
}