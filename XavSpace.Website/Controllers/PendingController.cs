using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using XavSpace.Facade.Managers;
using XavSpace.Website.Filters;
using XavSpace.Website.Hubs;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.Controllers
{
    [RestrictAccessTo(UserTypes="moderator")]
    public class PendingController : Controller
    {
        NoticeManager db = new NoticeManager();

        // GET: Pending
        public async Task<ActionResult> Index()
        {
            var pending_notices = await db.GetPendingAsync();
            List<PendingNoticeViewModel> vmlist = new List<PendingNoticeViewModel>();
            foreach(var n in pending_notices)
                vmlist.Add(NoticeMappings.To<PendingNoticeViewModel>(n));
            return View(vmlist);
        }

        [HttpPost]
        public async Task<ActionResult> Approve(int id)
        {
            await db.Approve(id);
            NotificationService.Instance.BroadcastNotification();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Disapprove(int id)
        {
            await db.Disapprove(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Disapprove(int id, string comment)
        {
            await db.Disapprove(id);
            return RedirectToAction("Index");
        }
    }
}
