using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XavSpace.Entities.Data;

namespace XavSpace.Website.ViewModels.JsonViewModels.Boards
{
    public class BoardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsOfficial { get; set; }

        public static BoardViewModel From(NoticeBoard noticeBoard)
        {
            return new BoardViewModel
            {
                Id = noticeBoard.NoticeBoardId,
                Title = noticeBoard.Title,
                Description = noticeBoard.Description,
                IsOfficial = noticeBoard.IsOfficial,
                IsMandatory = noticeBoard.IsMandatory
            };
        }
    }
}