using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Entities.Data
{
    /// <summary>
    /// The status of the notice
    /// </summary>
    public enum NoticeStatus
    {
    /// <summary>
        /// The notice hasn't been reviewed yet
        /// </summary>
        PendingApproval,
        /// <summary>
        /// The notice has been approved
        /// </summary>
        Approved,
        /// <summary>
        /// The notice has been disapproved
        /// </summary>
        Disapproved,
        /// <summary>
        /// The notice has been flagged by the moderator
        /// </summary>
        Flagged
    }

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
        public DateTime? DateReviewed { get; set; }

        /// <summary>
        /// If the notice has been approved
        /// </summary>
        public NoticeStatus Status { get; set; }

        /// <summary>
        /// If the approval of the current notice is pending
        /// </summary>
        [NotMapped]
        public bool IsPendingApproval { get { return DateReviewed == null; } }

        /// <summary>
        /// Comment by the moderator to the notice's Author
        /// </summary>
        public string ModeratorComment { get; set; }

        

        public Notice()
        {
            DateCreated = DateTime.UtcNow;
            DateReviewed = null;
            Status = NoticeStatus.PendingApproval;
            ModeratorComment = null;
        }

        /// <summary>
        /// Approves the notice
        /// </summary>
        public void Approve()
        {
            DateReviewed = DateTime.UtcNow;
            Status = NoticeStatus.Approved;
        }

        /// <summary>
        /// Disapproves the notice
        /// </summary>
        public void Disapprove()
        {
            DateReviewed = DateTime.UtcNow;
            Status = NoticeStatus.Disapproved;
        }
    }
}
