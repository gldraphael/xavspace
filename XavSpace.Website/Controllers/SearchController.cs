using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XavSpace.Entities.Data;
using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.Boards;
using XavSpace.Website.ViewModels.JsonViewModels.Search;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.Controllers
{
    public class SearchController : Controller
    {
        public async Task<ActionResult> Results(string q)
        {
            Tuple<List<NoticeBoardViewModel>, List<NoticeViewModel>> results = new Tuple<List<NoticeBoardViewModel>, List<NoticeViewModel>>(
                    new List<NoticeBoardViewModel>(),
                    new List<NoticeViewModel>()
                );

            using (NoticeBoardManager m = new NoticeBoardManager())
            {
                var boards = await m.SearchAsync(q);
                foreach (var i in boards)
                    results.Item1.Add(NoticeBoardMappings.To<NoticeBoardViewModel>(i));
            }

            using (NoticeManager m = new NoticeManager())
            {
                var notices = await m.SearchAsync(q);
                foreach (var i in notices)
                    results.Item2.Add(NoticeMappings.To<NoticeViewModel>(i));
            }

            return View(results);
        }

        public async Task<JsonResult> Boards(string q)
        {
            using (NoticeBoardManager m = new NoticeBoardManager())
            {
                var boards = await m.SearchAsync(q);
                var result = new List<SearchItemJVM>();
                foreach (var i in boards)
                    result.Add(new SearchItemJVM { value = i.Title, tokens = i.Title.Split(' ', ',', '.') });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> Notices(string q)
        {
            using (NoticeManager m = new NoticeManager())
            {
                var boards = await m.SearchAsync(q);
                var result = new List<SearchItemJVM>();
                foreach (var i in boards)
                    result.Add(new SearchItemJVM { value = i.Title, tokens = i.Title.Split(' ', ',', '.') });

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}