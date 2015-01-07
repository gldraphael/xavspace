using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace XavSpace.Website.Extensions
{
    public static class HtmlLayoutExtension
    {
        public static bool IsAboutPage(this HtmlHelper html, HttpContextBase context)
        {
            return context.Request.RequestContext.RouteData.Values["controller"].ToString() == "About" && context.Request.RequestContext.RouteData.Values["action"].ToString() == "Index";
        }
    }
}