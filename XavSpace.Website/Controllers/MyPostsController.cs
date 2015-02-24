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
                var approved = await nm.GetUserNoticesAsync(user.Id, 0, 6, Entities.Data.NoticeStatus.Approved);
                var pending = await nm.GetUserNoticesAsync(user.Id, 0, 6, Entities.Data.NoticeStatus.PendingApproval);
                var disapproved = await nm.GetUserNoticesAsync(user.Id, 0, 6, Entities.Data.NoticeStatus.Disapproved);

                Tuple<List<DetailedNoticeViewModel>, List<DetailedNoticeViewModel>, List<DetailedNoticeViewModel>> vm 
                    = new Tuple<List<DetailedNoticeViewModel>, List<DetailedNoticeViewModel>, List<DetailedNoticeViewModel>>(
                        new List<DetailedNoticeViewModel>(),
                        new List<DetailedNoticeViewModel>(),
                        new List<DetailedNoticeViewModel>());

                foreach (var n in approved)
                    vm.Item1.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

                foreach (var n in pending)
                    vm.Item2.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

                foreach (var n in pending)
                    vm.Item3.Add(NoticeMappings.To<DetailedNoticeViewModel>(n));

                return View(vm);
            }
        }

        // GET: /GetApproved?index=6&number=5
        public async Task<ActionResult> GetApproved(int? index, int? number)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                int i = index ?? 0;
                int n = number ?? 5;
                var user = await User.Identity.GetApplicationUserAsync();
                var notices = await nm.GetUserNoticesAsync(user.Id, i, n, Entities.Data.NoticeStatus.Approved);
                List<DetailedNoticeViewModel> vm = new List<DetailedNoticeViewModel>();

                foreach (var notice in notices)
                    vm.Add(NoticeMappings.To<DetailedNoticeViewModel>(notice));

                return View(vm);
            }
        }

        // GET: /GetPending?index=6&number=5
        public async Task<ActionResult> GetPending(int? index, int? number)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                int i = index ?? 0;
                int n = number ?? 5;
                var user = await User.Identity.GetApplicationUserAsync();
                var notices = await nm.GetUserNoticesAsync(user.Id, i, n, Entities.Data.NoticeStatus.PendingApproval);
                List<DetailedNoticeViewModel> vm = new List<DetailedNoticeViewModel>();

                foreach (var notice in notices)
                    vm.Add(NoticeMappings.To<DetailedNoticeViewModel>(notice));

                return View(vm);
            }
        }

        // GET: /GetAmended?index=6&number=5
        public async Task<ActionResult> GetAmended(int? index, int? number)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                int i = index ?? 0;
                int n = number ?? 5;
                var user = await User.Identity.GetApplicationUserAsync();
                var notices = await nm.GetUserNoticesAsync(user.Id, i, n, Entities.Data.NoticeStatus.Disapproved);
                List<DetailedNoticeViewModel> vm = new List<DetailedNoticeViewModel>();

                foreach (var notice in notices)
                    vm.Add(NoticeMappings.To<DetailedNoticeViewModel>(notice));

                return View(vm);
            }
        }
    }
}