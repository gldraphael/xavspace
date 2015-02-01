using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XavSpace.Facade.Managers;
using XavSpace.Website.ViewModels.Boards;

namespace XavSpace.Website.WebApi
{
    public class BoardsController : ApiController
    {
        // GET api/Boards
        public async Task<IEnumerable<NoticeBoardViewModel>> Get()
        {
            NoticeBoardManager nbm = new NoticeBoardManager();
            var boards = await nbm.GetOfficialBoardsAsync();
            List<NoticeBoardViewModel> vmlist = new List<NoticeBoardViewModel>();
            foreach(var nb in boards)
                vmlist.Add(NoticeBoardMappings.To<NoticeBoardViewModel>(nb));
            return vmlist;
        }
    }
}