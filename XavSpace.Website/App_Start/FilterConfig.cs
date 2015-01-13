using System.Web.Mvc;
using XavSpace.Website.Filters;

namespace XavSpace.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAndLogErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
