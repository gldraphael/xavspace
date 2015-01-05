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
using XavSpace.Website.ViewModels.Boards;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.Controllers
{
    public class BoardsController : Controller
    {
        private NoticeBoardManager db = new NoticeBoardManager();

        // GET: Boards
        public async Task<ActionResult> Index()
        {
            var x = await db.GetAsync();
            var list = new List<NoticeBoardIndexViewModel>();
            foreach (var i in x)
            {
                list.Add(NoticeBoardMappings.To<NoticeBoardIndexViewModel>(i));
            }
            return View(list);
        }

        // GET: Boards/View/5
        public async Task<ActionResult> View(int? id)
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
            return View(NoticeBoardMappings.To<NoticeBoardViewModel>(noticeBoard));
        }

        // GET: Boards/Create
        [ModeratorOnly]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        [HttpPost]
        [ModeratorOnly]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,IsOfficial,IsMandatory,Description")] NoticeBoardEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await db.AddAsync(NoticeBoardMappings.From(vm));
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Boards/Edit/5
        [ModeratorOnly]
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

            return View(NoticeBoardMappings.To<NoticeBoardEditViewModel>(noticeBoard));
        }

        // POST: Boards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModeratorOnly]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,IsOfficial,IsMandatory,Description")] NoticeBoardEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await db.UpdateAsync(NoticeBoardMappings.From(vm));
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Boards/Delete/5
        [ModeratorOnly]
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
            return View(NoticeBoardMappings.To<NoticeBoardIndexViewModel>(noticeBoard));
        }

        // POST: Boards/Delete/5
        [ModeratorOnly]
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

        [HttpPost]
        public ActionResult Post(int id)
        {
            NoticeViewModel vm = new NoticeViewModel
            {
                NoticeBoardId = id
            };
            return View(vm);
        }

        [HttpPost]
        [ActionName("ConfirmPost")]
        public async Task<ActionResult> Post(NoticeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await db.PostNotice(NoticeMappings.From(vm));
                return RedirectToAction("View", new { id = vm.NoticeBoardId });
            }
            return View(vm);
        }
    }
}
