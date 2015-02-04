using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "About");

            NoticeManager nm = new NoticeManager();
            var notices = await nm.GetDetailedAsync();

            List<DetailedNoticeViewModel> vms = new List<DetailedNoticeViewModel>();
            foreach (var n in notices)
                vms.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

            return View(vms);
        }
    }
}
