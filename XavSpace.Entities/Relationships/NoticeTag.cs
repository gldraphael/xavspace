using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XavSpace.Entities.Data;

namespace XavSpace.Entities.Relationships
{
    public class NoticeTag
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int NoticeTagId { get; set; }

        [Key]
        [Column(Order = 0)]
        public int NoticeId { get; set; }
        public Notice Notice { get; set; }

        [Key]
        [Column(Order = 1)]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
