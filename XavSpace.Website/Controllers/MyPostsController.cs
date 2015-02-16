using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XavSpace.Facade.Managers;
using XavSpace.Website.Extensions;
using XavSpace.Website.Filters;
using XavSpace.Website.ViewModels.JsonViewModels.Base;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.Controllers
{
    [RestrictAccessTo(UserTypes="staff,moderator")]
    public class MyPostsController : Controller
    {
        // GET: MyPosts
        public async Task<ActionResult> Index()
        {
            using(NoticeManager nm = new NoticeManager())
            {
                var notices = await nm.GetUserNoticesAsync((await User.Identity.GetApplicationUserAsync()).Id);
                List<DetailedNoticeViewModel> vm = new List<DetailedNoticeViewModel>();

                foreach (var n in notices)
                    vm.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

                return View(vm);
            }
        }
    }
}