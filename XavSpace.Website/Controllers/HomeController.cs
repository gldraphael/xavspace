using System.Web.Mvc;

namespace XavSpace.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
