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
            var notices = await nm.GetAsync();

            List<NoticeViewModel> vms = new List<NoticeViewModel>();
            foreach (var n in notices)
                vms.Add(NoticeMappings.ToNoticeViewModel(n));

            return View(vms);
        }
    }
}
