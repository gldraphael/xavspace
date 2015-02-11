using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XavSpace.Entities.Logs;
using XavSpace.Facade.Managers;

namespace XavSpace.Website.Filters
{
    public class HandleAndLogErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (filterContext.Exception is HttpException)
            {
                statusCode = (filterContext.Exception as HttpException).GetHttpCode();
            }
            else if (filterContext.Exception is UnauthorizedAccessException)
            {
                //to prevent login prompt in IIS
                // which will appear when returning 401.
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            else statusCode = 500;

#if !DEBUG
            // Log the exception
            ErrorLogManager errorLogManager = new ErrorLogManager();
            ErrorLog log = new ErrorLog()
            {
                Message = filterContext.Exception.Message,
                Name = filterContext.Exception.GetType().FullName,
                StatusCode = statusCode,
                StackTrace = filterContext.Exception.StackTrace,
                ActionName = (string)filterContext.RouteData.Values["action"],
                ControllerName = (string)filterContext.RouteData.Values["controller"]
            };
            errorLogManager.AddAsync(log).ContinueWith(x => x.Dispose());
#endif

            var result = CreateActionResult(filterContext, statusCode);
            filterContext.Result = result;

            // Prepare the response code.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, int statusCode)
        {
            var ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
            var statusCodeName = ((HttpStatusCode)statusCode).ToString();

            // filterContext.HttpContext.Response.StatusCode = statusCode;
            var viewName = SelectFirstView(ctx,
                // String.Format("~/Views/Error/{0}.cshtml", statusCodeName),
                // "~/Views/Error/General.cshtml",
                // statusCodeName,
                                           "~/Views/Error/Error.cshtml");

            if (statusCode == 500)
                viewName = "Error";

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            var result = new ViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
            };
            // result.ViewBag.StatusCode = statusCode;
            return result;
        }

        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }

        protected bool ViewExists(ControllerContext ctx, string name)
        {
            var result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }
    }
}
