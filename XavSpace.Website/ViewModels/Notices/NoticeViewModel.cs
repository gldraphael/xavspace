using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XavSpace.Website.ViewModels.Notices
{
    public class NoticeViewModel
    {
        public int Id { get; set; }
        public int NoticeBoardId { get; set; }

        /// <summary>
        /// The Title of the Notice
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the notice
        /// </summary>
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Description { get; set; }
    }
}