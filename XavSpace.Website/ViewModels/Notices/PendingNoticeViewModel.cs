using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using XavSpace.Entities.Data;

namespace XavSpace.Website.ViewModels.Notices
{
    public class PendingNoticeViewModel
    {
        public int Id { get; set; }
        [Required]
        public int NoticeBoardId { get; set; }

        public string NoticeBoardName { get; set; }

        /// <summary>
        /// The Title of the Notice
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Description of the notice
        /// </summary>
        /// 
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// The date when the notice was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Is the post HighPriority?
        /// </summary>
        public bool IsHighPriority { get; set; }

        /// <summary>
        /// The status of the notice
        /// </summary>
        public NoticeStatus Status { get; set; }

        /// <summary>
        /// Moderator comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The date when the notice was reviewed
        /// </summary>
        public DateTime? DateReviewed { get; set; }
    }
}
