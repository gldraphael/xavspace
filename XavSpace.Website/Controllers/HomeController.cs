using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.Notices;
using XavSpace.Website.Extensions;

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
            var user = await User.Identity.GetApplicationUserAsync();
            var notices = await nm.GetNewsFeedAsync(user.Id, 0, 6);

            List<DetailedNoticeViewModel> vms = new List<DetailedNoticeViewModel>();
            foreach (var n in notices)
                vms.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

            return View(vms);
        }

        // GET: /GetFeed?index=6&number=5
        public async Task<ActionResult> GetFeed(int? index, int? number)
        {
            
            int i = index ?? 0;
            int n = number ?? 5;

            NoticeManager nm = new NoticeManager();
            var user = await User.Identity.GetApplicationUserAsync();
            var notices = await nm.GetNewsFeedAsync(user.Id, i, n);

            List<DetailedNoticeViewModel> vms = new List<DetailedNoticeViewModel>();
            foreach (var notice in notices)
                vms.Add(NoticeMappings.To<DetailedNoticeViewModel>(notice));
            
            return View(vms);
        }
    }
}
