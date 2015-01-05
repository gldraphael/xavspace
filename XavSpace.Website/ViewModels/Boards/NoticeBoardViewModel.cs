using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using XavSpace.Website.ViewModels.Notices;

namespace XavSpace.Website.ViewModels.Boards
{
    public class NoticeBoardViewModel
    {
        [Display(Name="")]
        public int Id { get; set; }

        [Display(Name = "Board Name")]
        public string Title { get; set; }

        public bool IsOfficial { get; set; }

        public bool IsMandatory { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public List<NoticeViewModel> Notices { get; set; }

        public NoticeBoardViewModel()
        {
            Notices = new List<NoticeViewModel>();
        }
    }
}
