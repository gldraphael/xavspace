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
using XavSpace.Website.Extensions;

namespace XavSpace.Website.Controllers
{
    public class NoticesController : Controller
    {
        public async Task<JsonResult> Delete(int noticeId)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                var postedBy = await nm.PostedBy(noticeId);
                var current = await User.Identity.GetApplicationUserAsync();
                var notice = await nm.GetAsync(noticeId);

                if ((postedBy == current && notice.IsPendingApproval)
                    || await User.Identity.IsModeratorAsync())
                {
                    var res = await nm.DeleteAsync(noticeId);
                    if (res > 0)
                        return Json(JsonViewModel.Success);
                }
            }
            return Json(JsonViewModel.Error);
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
        //    Notice notice = await db.Notices.FindAsync(id);
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
