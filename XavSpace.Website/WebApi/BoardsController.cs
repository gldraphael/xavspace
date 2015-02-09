using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XavSpace.Entities.Data;
using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.Boards;
using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.WebApi
{
    public class BoardsController : ApiController
    {
        // GET: /Boards
        public async Task<IEnumerable<NoticeBoardViewModel>> Get()
        {
            NoticeBoardManager nbm = new NoticeBoardManager();
            var boards = await nbm.GetOfficialBoardsAsync();
            List<NoticeBoardViewModel> vmlist = new List<NoticeBoardViewModel>();
            foreach (var nb in boards)
                vmlist.Add(NoticeBoardMappings.To<NoticeBoardViewModel>(nb));
            return vmlist;
        }

        // Get: /Boards/Get/5
        public async Task<IHttpActionResult> Get(int boardId)
        {
            NoticeBoardManager nm = new NoticeBoardManager();

            var nb = await nm.GetAsync(boardId);
            if (nb == null)
                return NotFound();
            
            return Ok(NoticeBoardMappings.To<NoticeBoardViewModel>(nb));
        }
    }
}