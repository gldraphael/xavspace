using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using XavSpace.Website.Extensions;

namespace XavSpace.Website.Filters
{

    public class RestrictAccessToAttribute : AuthorizeAttribute
    {
        private string userTypes;
        public string UserTypes
        {
            get { return userTypes ?? string.Empty; }
            set
            {
                userTypes = value.ToLower();
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (AuthorizeCore(filterContext.HttpContext))
                if (!IsValid(filterContext))
                    throw new HttpException((int)HttpStatusCode.Forbidden, "Access Denied");
        }

        private bool IsValid(AuthorizationContext filterContext)
        {
            if (String.IsNullOrWhiteSpace(userTypes))
                return true;

            if (UserTypes.Contains("moderator"))
                if (filterContext.HttpContext.User.Identity.IsModerator())
                    return true;

            if (UserTypes.Contains("staff"))
                if (filterContext.HttpContext.User.Identity.IsStaff())
                    return true;

            return false;
        }
    }

    internal class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            // Set the response code to 403
            context.HttpContext.Response.StatusCode = 403;
        }
    }
}