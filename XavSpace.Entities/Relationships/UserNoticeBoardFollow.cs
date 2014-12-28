﻿using System;
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
    public class UserNoticeBoardFollow
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserNoticeBoardFollowId { get; set; }

        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 1)]
        public int NoticeBoardId { get; set; }
        public NoticeBoard NoticeBoard { get; set; }
    }
}
