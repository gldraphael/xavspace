using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using XavSpace.Entities.Data;

namespace XavSpace.Website.ViewModels.Notices
{
    public class NoticePosterViewModel
    {
        public int Id { get; set; }
        public int NoticeBoardId { get; set; }

        /// <summary>
        /// The name of the notice board
        /// </summary>
        public string NoticeBoardName { get; set; }

        /// <summary>
        /// The Title of the Notice
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the notice
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// The date when the notice was created
        /// </summary>
        public DateTime? DateCreated { get; set; }
        /// <summary>
        /// Is the post HighPriority?
        /// </summary>
        public bool isHighPriority { get; set; }

        public NoticeStatus Status { get; set; }

        public string ModeratorComment { get; set; }
    }
}