using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XavSpace.Entities.Data;
using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.JsonViewModels.Boards;
using XavSpace.Website.Extensions;
using XavSpace.Website.ViewModels.JsonViewModels.Base;
using XavSpace.Entities.Relationships;

namespace XavSpace.Website.WebApi
{
    public class BoardsController : ApiController
    {
        // GET: /Boards
        public async Task<IEnumerable<BoardViewModel>> Get()
        {
            NoticeBoardManager nbm = new NoticeBoardManager();
            var boards = await nbm.GetAsync();
            List<BoardViewModel> vmlist = new List<BoardViewModel>();
            foreach (var nb in boards)
                vmlist.Add(BoardViewModel.From(nb));
            return vmlist;
        }

        // Get: /Boards/Get/5
        public async Task<IHttpActionResult> Get(int id)
        {
            NoticeBoardManager nm = new NoticeBoardManager();

            var nb = await nm.GetAsync(id);
            if (nb == null)
                return NotFound();

            return Ok(BoardViewModel.From(nb));
        }

        // GET: /Boards/Subscribed
        public async Task<IEnumerable<BoardViewModel>> Subscribed()
        {
            NoticeBoardManager nbm = new NoticeBoardManager();
            var boards = await nbm.GetAsync();
            List<BoardViewModel> vmlist = new List<BoardViewModel>();
            foreach (var nb in boards)
                if (await User.Identity.IsSubscribedToAsync(nb.NoticeBoardId))
                    vmlist.Add(BoardViewModel.From(nb));
            return vmlist;
        }

        // GET: /Boards/NotSubscribed
        public async Task<IEnumerable<BoardViewModel>> NotSubscribed()
        {
            NoticeBoardManager nbm = new NoticeBoardManager();
            var boards = await nbm.GetAsync();
            List<BoardViewModel> vmlist = new List<BoardViewModel>();
            foreach (var nb in boards)
                if (!await User.Identity.IsSubscribedToAsync(nb.NoticeBoardId))
                    vmlist.Add(BoardViewModel.From(nb));
            return vmlist;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Subscribe(int id)
        {
            using (NoticeBoardManager db = new NoticeBoardManager())
            {
                var board = await db.GetAsync(id);
                RelationshipManager rm = new RelationshipManager();
                int result = await rm.AddAsync(
                        new UserNoticeBoardFollow
                        {
                            NoticeBoardId = id,
                            UserId = (await User.Identity.GetApplicationUserAsync()).Id
                        }
                    );
                if (result > 0)
                    return Ok(JsonViewModel.Success);

                // shouldn't reach here unless a concurrency issue occurs
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Unsubscribe(int id)
        {
            try
            {
                using (NoticeBoardManager db = new NoticeBoardManager())
                {
                    var board = await db.GetAsync(id);
                    RelationshipManager rm = new RelationshipManager();
                    int result = await rm.DeleteAsync(
                            new UserNoticeBoardFollow
                            {
                                NoticeBoardId = id,
                                UserId = (await User.Identity.GetApplicationUserAsync()).Id
                            }
                        );
                    if (result > 0)
                        return Ok(JsonViewModel.Success);
                }
            }
            catch
            {
                return InternalServerError();
            }
            return NotFound();
        }
    }
}
