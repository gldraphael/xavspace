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
        [Required]
        public int NoticeBoardId { get; set; }


        /// <summary>
        /// The Title of the Notice
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Description of the notice
        /// </summary>
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// The date when the notice was created
        /// </summary>
        public DateTime? DateCreated { get; set; }
        /// <summary>
        /// Is the post HighPriority?
        /// </summary>
        public bool isHighPriority { get; set; }
    }
}