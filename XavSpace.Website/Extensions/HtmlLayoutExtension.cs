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


        public static string ToString(this DateTime? inputDate, bool showOnlyDate)
        {
            if (showOnlyDate)
            {
                string returnString;

                if (inputDate == DateTime.MinValue)
                {
                    returnString = "N/A";
                }

                else
                {
                    returnString = inputDate.Value.ToString("dd  MMMM yyyy");
                    
                }

                return returnString;
            }
            return inputDate.ToString();
        }

        public static string Friendly(this DateTime? dt)
        {
            string suffix;

            switch (dt.Value.Day)
            {
                case 1:
                case 21:
                case 31:
                    suffix = "st";
                    break;
                case 2:
                case 22:
                    suffix = "nd";
                    break;
                case 3:
                case 23:
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }

            return string.Format("{1}{2} {0:MMMM}, {0:yyyy}", dt, dt.Value.Day, suffix);
        }

       
    }
}