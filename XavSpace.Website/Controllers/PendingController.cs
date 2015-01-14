using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XavSpace.Facade.Managers;
using XavSpace.Website.Filters;

namespace XavSpace.Website.Controllers
{
    [ModeratorOnly]
    public class PendingController : Controller
    {
        NoticeManager db = new NoticeManager();

        // GET: Pending
        public async Task<ActionResult> Index()
        {
            return View(await db.GetPendingAsync());
        }
    }
}