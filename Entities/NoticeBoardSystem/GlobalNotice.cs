using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace XavSpace.Entities.NoticeBoardSystem
{
    /// <summary>
    /// Represents a notice
    /// ! Temporary class - to be removed 
    /// </summary>
    public class GlobalNotice
    {
        [Key]
        public string GlobalNoticeId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [NotMapped]
        [DataType(DataType.DateTime)]
        public DateTime? DateWithTime { get { return Date; } }

        public GlobalNotice()
        {
            GlobalNoticeId = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            
        }
    }

    /// <summary>
    /// Represents a database connection
    /// ! Temporary class - to be removed later
    /// </summary>
    public class GlobalNoticeDbContext : DbContext 
    {
        public DbSet<GlobalNotice> Notices { get; set; }

        public GlobalNoticeDbContext()
            : base("SEProj")
        { }
    }
}
