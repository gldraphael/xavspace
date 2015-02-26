using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XavSpace.Website.ViewModels.JsonViewModels.Boards
{
    public class DetailedBoardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsMandatory { get; set; }

        public List<Notices.NoticeViewModel> Notices { get; set; }

        public DetailedBoardViewModel()
        {
            Notices = new List<Notices.NoticeViewModel>();
        }
    }
}