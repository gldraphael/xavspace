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
using XavSpace.Website.Filters;

namespace XavSpace.Website.Controllers
{
    [ModeratorOnly]
    public class BoardsController : Controller
    {
        private NoticeBoardManager db = new NoticeBoardManager();

        // GET: Boards
        public async Task<ActionResult> Index()
        {
            return View(await db.GetAsync());
        }

        // GET: Boards/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticeBoard noticeBoard = await db.GetAsync(id.Value);
            if (noticeBoard == null)
            {
                return HttpNotFound();
            }
            return View(noticeBoard);
        }

        // GET: Boards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NoticeBoardId,Title,IsOfficial,IsMandatory,Description")] NoticeBoard noticeBoard)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(noticeBoard);
                return RedirectToAction("Index");
            }

            return View(noticeBoard);
        }

        // GET: Boards/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticeBoard noticeBoard = await db.GetAsync(id.Value);
            if (noticeBoard == null)
            {
                return HttpNotFound();
            }
            return View(noticeBoard);
        }

        // POST: Boards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NoticeBoardId,Title,IsOfficial,IsMandatory,Description")] NoticeBoard noticeBoard)
        {
            if (ModelState.IsValid)
            {
                await db.UpdateAsync(noticeBoard);
                return RedirectToAction("Index");
            }
            return View(noticeBoard);
        }

        // GET: Boards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticeBoard noticeBoard = await db.GetAsync(id.Value);
            if (noticeBoard == null)
            {
                return HttpNotFound();
            }
            return View(noticeBoard);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
