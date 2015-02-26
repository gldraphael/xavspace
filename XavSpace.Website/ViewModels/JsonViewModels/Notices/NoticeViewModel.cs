using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XavSpace.Entities.Data;

namespace XavSpace.Website.ViewModels.JsonViewModels.Notices
{
    public class NoticeViewModel
    {
        public int Id { get; set; }
        public string Board { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool HighPriority { get; set; }

        public static NoticeViewModel From(Notice notice)
        {
            return new NoticeViewModel
            {
                Id = notice.NoticeId,
                Title = notice.Title,
                Content = notice.Description,
                HighPriority = notice.HighPriority,
                Board = notice.NoticeBoard.Title
            };
        }
    }
}