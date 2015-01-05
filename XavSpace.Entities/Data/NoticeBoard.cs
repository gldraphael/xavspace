using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XavSpace.Entities.Users;

namespace XavSpace.Entities.Data
{
    public class NoticeBoard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoticeBoardId { get; set; }

        /// <summary>
        /// The Notice Board
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Official Notice Boards are those that require approval
        /// </summary>
        public bool IsOfficial {get;set;}

        /// <summary>
        /// Mandatory notice boards have to be followed.
        /// </summary>
        public bool IsMandatory { get; set; }

        /// <summary>
        /// Something about the Notice Board
        /// </summary>
        public string Description { get; set; }

        public virtual List<Notice> Notices { get; set; }
    }
}
