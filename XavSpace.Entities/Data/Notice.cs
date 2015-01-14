using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XavSpace.Entities.Data;

namespace XavSpace.Entities.Data
{
    /// <summary>
    /// Represents a notice
    /// </summary>
    public class Notice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoticeId { get; set; }

        public int NoticeBoardId { get; set; }
        public NoticeBoard NoticeBoard { get; set; }

        /// <summary>
        /// The Title of the Notice
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the notice
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the date when the notice was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets whether the post is a high priority post
        /// </summary>
        public bool HighPriority { get; set; }

        /// <summary>
        /// Gets or Sets the date when the notice was approved. Null if the notice has not been approved.
        /// </summary>
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// If the notice has been approved
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// If the approval of the current notice is pending
        /// </summary>
        [NotMapped]
        public bool IsPendingApproval { get { return DateApproved == null; } }

        /// <summary>
        /// Comment by the moderator to the notice's Author
        /// </summary>
        public string ModeratorComment { get; set; }

        public Notice()
        {
            DateCreated = DateTime.UtcNow;
            DateApproved = null;
            IsApproved = false;
            ModeratorComment = null;
        }

        /// <summary>
        /// Approves the notice
        /// </summary>
        public void Approve()
        {
            DateApproved = DateTime.UtcNow;
            IsApproved = true;
        }

        /// <summary>
        /// Disapproves the notice
        /// </summary>
        public void Disapprove()
        {
            DateApproved = DateTime.UtcNow;
            IsApproved = false;
        }
    }
}
