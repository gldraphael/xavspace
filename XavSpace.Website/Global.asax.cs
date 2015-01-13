using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;
using System;

using XavSpace.Facade.Managers;
using XavSpace.Entities.Logs;
using XavSpace.Website.Controllers;

namespace XavSpace.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
#if !DEBUG
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Removes the 'Server' HTTP header
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }
        }
#endif

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Context.IsCustomErrorEnabled)
                ShowCustomErrorPage(Server.GetLastError());
        }

        private void ShowCustomErrorPage(Exception exception)
        {
            HttpException httpException = exception as HttpException;
            if (httpException == null)
                httpException = new HttpException(500, "Internal Server Error", exception);

            Response.Clear();
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("fromAppErrorEvent", true);

            switch (httpException.GetHttpCode())
            {
                case 403:
                    routeData.Values.Add("action", "AccessDenied");
                    break;

                case 404:
                    routeData.Values.Add("action", "NotFound");
                    break;

                case 500:
                    routeData.Values.Add("action", "InternalError");
                    break;

                case 502:
                    routeData.Values.Add("action", "Unavailable");
                    break;

                default:
                    routeData.Values.Add("action", "Index");
                    routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
                    break;
            }

#if !DEBUG
            ErrorLogManager errorLogManager = new ErrorLogManager();
            ErrorLog log = new ErrorLog()
            {
                Message = exception.Message,
                Name = exception.GetType().FullName,
                StatusCode = httpException.GetHttpCode(),
                StackTrace = exception.StackTrace
            };
            errorLogManager.AddAsync(log).ContinueWith(x=>x.Dispose());
#endif

            Server.ClearError();
            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
