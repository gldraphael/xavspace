using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XavSpace.Website.Filters;

namespace XavSpace.Website.Controllers
{
    public class ErrorController : Controller
    {
        // 500
        [PreventDirectAccessToErrorAction]
        public ActionResult InternalError()
        {
            Response.StatusCode = 500;
            return View("Error");
        }

        // 502
        [PreventDirectAccessToErrorAction]
        public ActionResult Unavailable()
        {
            Response.StatusCode = 502;
            return View("Error");
        }

        // 403
        [PreventDirectAccessToErrorAction]
        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            return View("Error");
        }

        // 404
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("Error");
        }

        // any other error code
        [PreventDirectAccessToErrorAction]
        public ActionResult Index(int httpStatusCode)
        {
            Response.StatusCode = httpStatusCode;
            return View("Error", httpStatusCode);
        }
    }
}