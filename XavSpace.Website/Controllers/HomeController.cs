using System.Web.Mvc;

namespace XavSpace.Website.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index", "About");
        }
    }
}
