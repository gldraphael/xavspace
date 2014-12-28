using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XavSpace.Entities.Data;
using XavSpace.Entities.Users;

namespace XavSpace.Entities.Relationships
{
    public class UserNoticePost
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int UserNoticePostId { get; set; }

        [Key]
        [Column(Order=0)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 1)]
        public int NoticeId { get; set; }
        public Notice Notice { get; set; }
    }
}
