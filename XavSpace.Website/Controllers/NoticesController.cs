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
                        //return Json(JsonViewModel.Success);
                       
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

                if(notice.Status == NoticeStatus.Approved)
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
                    //return Json(JsonViewModel.Success);
                }
            }
            // return Json(JsonViewModel.Error);
            return RedirectToAction("Index", "MyPosts");
        }

        #region Old COde
        //private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: Notices
        //public async Task<ActionResult> Index()
        //{
        //    var notices = db.Notices.Include(n => n.NoticeBoard);
        //    return View(await notices.ToListAsync());
        //}

        //// GET: Notices/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Notice notice = await db.Notices.FindAsync(id);
        //    if (notice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notice);
        //}

        //// GET: Notices/Create
        //public ActionResult Create()
        //{
        //    ViewBag.NoticeBoardId = new SelectList(db.NoticeBoards, "NoticeBoardId", "Title");
        //    return View();
        //}

        //// POST: Notices/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "NoticeId,NoticeBoardId,Title,Description,HighPriority")] Notice notice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Notices.Add(notice);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.NoticeBoardId = new SelectList(db.NoticeBoards, "NoticeBoardId", "Title", notice.NoticeBoardId);
        //    return View(notice);
        //}

        //// GET: Notices/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Notice notice = await  db.Notices.FindAsync(id);
        //    if (notice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.NoticeBoardId = new SelectList(db.NoticeBoards, "NoticeBoardId", "Title", notice.NoticeBoardId);
        //    return View(notice);
        //}

        //// POST: Notices/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "NoticeId,NoticeBoardId,Title,Description,DateCreated,DateApproved,HighPriority")] Notice notice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(notice).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.NoticeBoardId = new SelectList(db.NoticeBoards, "NoticeBoardId", "Title", notice.NoticeBoardId);
        //    return View(notice);
        //}

        //// GET: Notices/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Notice notice = await db.Notices.FindAsync(id);
        //    if (notice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notice);
        //}

        //// POST: Notices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Notice notice = await db.Notices.FindAsync(id);
        //    db.Notices.Remove(notice);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
#endregion
    }
}
