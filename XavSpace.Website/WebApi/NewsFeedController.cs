using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XavSpace.Entities.Users;
using XavSpace.Facade.Managers;
using XavSpace.Website.Extensions;
using XavSpace.Website.ViewModels.JsonViewModels.Notices;

namespace XavSpace.Website.WebApi
{
    [Authorize]
    public class NewsFeedController : ApiController
    {
        private NoticeManager nm = new NoticeManager();

        // GET: /NewsFeed
        public async Task<IEnumerable<NoticeViewModel>> Get()
        {
            return await Get(0, 5);
        }

        // GET: /NewsFeed?index=0&number=5
        public async Task<IEnumerable<NoticeViewModel>> Get(int index, int number)
        {
            var user = await User.Identity.GetApplicationUserAsync();
            var notices = await nm.GetNewsFeedAsync(user.Id, index, number);
            List<NoticeViewModel> vm = new List<NoticeViewModel>();

            foreach (var i in notices)
                vm.Add(NoticeViewModel.From(i));

            return vm;
        }
    }
}
