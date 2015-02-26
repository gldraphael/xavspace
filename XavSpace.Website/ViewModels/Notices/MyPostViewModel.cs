using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XavSpace.Website.ViewModels.Notices
{
    public class MyPostViewModel
    {
        public List<DetailedNoticeViewModel> ApprovedPosts { get; set; }
        public List<DetailedNoticeViewModel> PendingPosts { get; set; }
        public List<AmendedNoticeViewModel> AmendedPosts { get; set; }

        public MyPostViewModel()
        {
            ApprovedPosts = new List<DetailedNoticeViewModel>();
            PendingPosts = new List<DetailedNoticeViewModel>();
            AmendedPosts = new List<AmendedNoticeViewModel>();
        }
    }
}