using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XavSpace.Entities.Logs;
using XavSpace.Facade.Managers;

namespace XavSpace.Website.Controllers
{
    [Authorize(Roles = "Tester")]
    public class ErrorLogController : Controller
    {
        private ErrorLogManager db = new ErrorLogManager();

        // GET: ErrorLogs
        public async Task<ActionResult> Index()
        {
            var logs = from x in await db.GetAsync()
                       orderby x.TimeStampUtc descending
                       select x;

            return View(logs);
        }

        // GET: ErrorLogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorLog errorLog = await db.GetAsync(id.Value);
            if (errorLog == null)
            {
                return HttpNotFound();
            }
            return View(errorLog);
        }

        // GET: ErrorLogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorLog errorLog = await db.GetAsync(id.Value);
            if (errorLog == null)
            {
                return HttpNotFound();
            }
            return View(errorLog);
        }

        // POST: ErrorLogs/Delete/5
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

        public ErrorLogController()
        {
            ViewBag.Role = "Tester";
        }
    }
}