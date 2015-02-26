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
using XavSpace.Entities.Relationships;
using XavSpace.Facade.Managers;
using XavSpace.Website.Filters;
using XavSpace.Website.ViewModels.Boards;
using XavSpace.Website.ViewModels.Notices;
using XavSpace.Website.Extensions;
using XavSpace.Website.ViewModels.JsonViewModels.Base;

namespace XavSpace.Website.Controllers
{
    public class BoardsController : Controller
    {
        private NoticeBoardManager db = new NoticeBoardManager();

        // GET: Boards
        /// <summary>
        /// Displays all the official notice boards that the user is subscribed to
        /// </summary>
        public async Task<ActionResult> Index()
        {
            var x = await db.GetOfficialBoardsAsync();
            var list = new List<NoticeBoardIndexViewModel>();
            foreach (var i in x)
            {
                var temp = NoticeBoardMappings.To<NoticeBoardIndexViewModel>(i);
                temp.IsSubscribed = await User.Identity.IsSubscribedToAsync(temp.Id);
                //if (temp.IsSubscribed)
                //{
                    list.Add(temp);
                //}
            }
            return View(list);
        }

        // GET: Official/GetBoards?index=6&number=5
        public async Task<ActionResult> GetBoards(int? index, int? number)
        {
            int i = index ?? 0;
            int n = number ?? 5;

            var boards = await db.GetOfficialBoardsAsync();
            var list = new List<NoticeBoardIndexViewModel>();
            foreach (var nb in boards)
            {
                var temp = NoticeBoardMappings.To<NoticeBoardIndexViewModel>(nb);
                temp.IsSubscribed = await User.Identity.IsSubscribedToAsync(temp.Id);
                if (!temp.IsSubscribed)
                {
                    list.Add(temp);
                }
            }

            IEnumerable<NoticeBoardIndexViewModel> finalList = list.OrderBy(nb => nb.Title);

            finalList = finalList.Skip(i);
            finalList = finalList.Take(n);

            return View(finalList);
        }

        // GET: Boards/View/5
        /// <summary>
        /// Displays a notice board
        /// </summary>
        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticeBoard noticeBoard = await db.GetOfficialBoardAsync(id.Value);

            if (noticeBoard == null)
            {
                return HttpNotFound();
            }

            if (await User.Identity.IsSubscribedToAsync(id.Value))
                ViewBag.Subscribed = true;
            else ViewBag.Subscribed = false;

            return View(NoticeBoardMappings.To<NoticeBoardViewModel>(noticeBoard));
        }


        // GET: Boards/Create
        /// <summary>
        /// Create an official notice board
        /// </summary>
        [RestrictAccessTo(UserTypes = "moderator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        /// <summary>
        /// Creates an official notice board
        /// </summary>
        [HttpPost]
        [RestrictAccessTo(UserTypes = "moderator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,IsMandatory,Description")] NoticeBoardEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var notice = NoticeBoardMappings.From(vm);
                notice.IsOfficial = true;
                await db.AddAsync(notice);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Boards/Edit/5
        /// <summary>
        /// Edits a notice board
        /// </summary>
        [RestrictAccessTo(UserTypes = "moderator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NoticeBoard noticeBoard = await db.GetOfficialBoardAsync(id.Value);
            if (noticeBoard == null)
            {
                return HttpNotFound();
            }

            return View(NoticeBoardMappings.To<NoticeBoardEditViewModel>(noticeBoard));
        }

        // POST: Boards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RestrictAccessTo(UserTypes = "moderator")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,IsMandatory,Description")] NoticeBoardEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var notice = NoticeBoardMappings.From(vm);
                notice.IsOfficial = true;
                await db.UpdateAsync(notice);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RestrictAccessTo(UserTypes = "moderator")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.DeleteOrArchiveAsync(id);
            return RedirectToAction("Index");
        }


        #region Post Notice

        [HttpPost]
        [RestrictAccessTo(UserTypes = "staff,moderator")]
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
        [RestrictAccessTo(UserTypes = "staff,moderator")]
        public async Task<ActionResult> Post(NoticeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                NoticeManager nm = new NoticeManager();
                await nm.AddAsync(NoticeMappings.From(vm), await this.User.Identity.GetApplicationUserAsync());
                return RedirectToAction("View", new { id = vm.NoticeBoardId });
            }
            return View(vm);
        }

        #endregion

        // Get:Boards/EditNotice/5
        
        public async Task<ActionResult> EditNotice(int id)
        {
            using (NoticeManager nm = new NoticeManager())
            {
                var notice = await nm.GetAsync(id);
                var board = await db.GetAsync(notice.NoticeBoardId);

                // access denied if user is not a staff and the notice board is official
                if (board.IsOfficial)
                {
                    if (!await User.Identity.IsStaffAsync())
                        throw new HttpException(403, "Access Denied");
                }

                var vm = NoticeMappings.To<NoticeViewModel>(notice);
                return View(vm);
            }
        }

        #region Subscribe
        // POST: Boards/Subscribe?noticeboardId=5
        [HttpPost]
        public async Task<JsonResult> Subscribe(int noticeboardId)
        {
            var board = await db.GetAsync(noticeboardId);
            RelationshipManager rm = new RelationshipManager();
            int result = await rm.AddAsync(
                    new UserNoticeBoardFollow
                    {
                        NoticeBoardId = noticeboardId,
                        UserId = (await User.Identity.GetApplicationUserAsync()).Id
                    }
                );
            if (result > 0)
                return Json(JsonViewModel.Success, JsonRequestBehavior.DenyGet);
            return Json(JsonViewModel.Error, JsonRequestBehavior.DenyGet);
        }

        // POST: Boards/Unsubscribe?noticeboardId=5
        [HttpPost]
        public async Task<JsonResult> Unsubscribe(int noticeboardId)
        {
            try
            {
                var board = await db.GetAsync(noticeboardId);
                RelationshipManager rm = new RelationshipManager();
                int result = await rm.DeleteAsync(
                        new UserNoticeBoardFollow
                        {
                            NoticeBoardId = noticeboardId,
                            UserId = (await User.Identity.GetApplicationUserAsync()).Id
                        }
                    );
                if (result > 0)
                    return Json(JsonViewModel.Success, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return Json(JsonViewModel.Error, JsonRequestBehavior.DenyGet);
            }
            return Json(JsonViewModel.Error, JsonRequestBehavior.DenyGet);
        }
        #endregion


        #region
        // Get: Boards/Notice/5
        [AllowAnonymous]
        public async Task<ActionResult> Notice(int? id)
        {
            if (!id.HasValue)
                return HttpNotFound();

            using (NoticeManager nm = new NoticeManager())
            {
                var n = await nm.GetDetailedAsync(id.Value);

                if (n == null)
                    return HttpNotFound();

                return View(NoticeMappings.To<DetailedNoticeViewModel>(n));
            }
        }
        #endregion

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
