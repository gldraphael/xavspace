using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XavSpace.Entities.Data;
using XavSpace.Entities.Data;

namespace XavSpace.Entities.Relationships
{
    public class NoticeTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoticeTagId { get; set; }

        public int NoticeId { get; set; }
        public Notice Notice { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
