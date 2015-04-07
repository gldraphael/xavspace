using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

using XavSpace.Entities.Data;
using XavSpace.Entities.Users;
using XavSpace.Entities.Relationships;
using XavSpace.Entities.Logs;

namespace XavSpace.DataAccess.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("XSLocal", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Data
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Notice> Notices { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        // Relationship mappings
        public DbSet<UserNoticeBoardFollow> UserBoardFollowingRelationship { get; set; }
        public DbSet<UserNoticePost> UserNoticePostRelationship { get; set; }
    }
}
