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
                var user = await User.Identity.GetApplicationUserAsync();
                var notices = await nm.GetUserNoticesAsync(user.Id, 0, 6);
                List<DetailedNoticeViewModel> vm = new List<DetailedNoticeViewModel>();

                foreach (var n in notices)
                    vm.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

                return View(vm);
            }
        }
        // GET: /GetFeed?index=6&number=5
        public async Task<ActionResult> GetFeed(int? index, int? number)
        {

            

            using (NoticeManager nm = new NoticeManager())
            {
                int i = index ?? 0;
                int n = number ?? 5;
                var user = await User.Identity.GetApplicationUserAsync();
                var notices = await nm.GetUserNoticesAsync(user.Id,i,n);
                List<DetailedNoticeViewModel> vm = new List<DetailedNoticeViewModel>();

                foreach (var notice in notices)
                    vm.Add(NoticeMappings.To<DetailedNoticeViewModel>(notice));

                return View(vm);
            }
        }
    }
}