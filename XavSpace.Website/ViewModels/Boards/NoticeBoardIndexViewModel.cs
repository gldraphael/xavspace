using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XavSpace.Website.ViewModels.Boards
{
    public class NoticeBoardIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }

        public bool IsSubscribed { get; set; }
    }
}