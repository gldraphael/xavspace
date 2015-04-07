using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using XavSpace.DataAccess.DbContexts;
using XavSpace.Entities.Data;
using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.JsonViewModels.Base;
using XavSpace.Website.ViewModels.Notices;
using XavSpace.Website.Extensions;

namespace XavSpace.Website.Controllers
{
    public class NoticesController : Controller
    {
        // POST: /Notices/Delete?noticeId=5
        [HttpPost]
        public async Task<ActionResult> Delete(int noticeId)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                var postedBy = await nm.PostedBy(noticeId);
                var current = await User.Identity.GetApplicationUserAsync();
                var notice = await nm.GetAsync(noticeId);

                if ((postedBy.Id == current.Id && notice.IsPendingApproval)
                    || await User.Identity.IsModeratorAsync())
                {
                    var res = await nm.DeleteAsync(noticeId);
                    //if (res > 0) ;
                    //  return Json(JsonViewModel.Success);
                }

            }
            // return Json(JsonViewModel.Error);
            return RedirectToAction("Index", "Home");
        }

        // POST: /Notices/Edit?noticeId=5

        [HttpPost]
        public async Task<ActionResult> Edit(NoticeViewModel vm)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                var postedBy = await nm.PostedBy(vm.Id);
                var current = await User.Identity.GetApplicationUserAsync();
                var notice = await nm.GetAsync(vm.Id);

                if (notice.Status == NoticeStatus.Approved)
                    return Json(JsonViewModel.Error);

                if ((postedBy.Id == current.Id && notice.IsPendingApproval)
                    || await User.Identity.IsModeratorAsync())
                {
                    notice.NoticeId = vm.Id;
                    notice.HighPriority = vm.isHighPriority;
                    notice.NoticeBoardId = vm.NoticeBoardId;
                    notice.Title = vm.Title;
                    notice.Description = vm.Description;
                    notice.Status = NoticeStatus.PendingApproval;
                    var res = await nm.UpdateAsync(notice);

                    //if (res > 0) ;
                    //  return Json(JsonViewModel.Success);
                }
            }
            // return Json(JsonViewModel.Error);
            return RedirectToAction("Index", "MyPosts");
        }
    }
}
