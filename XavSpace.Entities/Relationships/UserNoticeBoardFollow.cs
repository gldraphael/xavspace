using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XavSpace.Entities.Data;
using XavSpace.Entities.Users;

namespace XavSpace.Entities.Relationships
{
    public class UserNoticeBoardFollow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserNoticeBoardFollowId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int NoticeBoardId { get; set; }
        public NoticeBoard NoticeBoard { get; set; }
    }
}
