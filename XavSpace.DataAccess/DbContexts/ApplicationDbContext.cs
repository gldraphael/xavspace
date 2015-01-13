using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

using XavSpace.DataAccess.DbInitializers;
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

        //static ApplicationDbContext()
        //{
        //    // Set the database intializer which is run once during application start
        //    // This seeds the database with admin user credentials and admin role
        //    Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    //modelBuilder.Entity<IdentityUser>().ToTable("Users");
        //    //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        //    //modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        //    //modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        //    //modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

        //    modelBuilder.Entity<IdentityUser>().ToTable("MyUsers").Property(p => p.Id).HasColumnName("UserId");
        //    modelBuilder.Entity<ApplicationUser>().ToTable("MyUsers").Property(p => p.Id).HasColumnName("UserId");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("MyUserRoles");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("MyUserLogins");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("MyUserClaims");
        //    modelBuilder.Entity<IdentityRole>().ToTable("MyRoles");
        //}

        // Data
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Notice> Notices { get; set; }


        public DbSet<ErrorLog> ErrorLogs { get; set; }


        // Relationship mappings
        public DbSet<NoticeTag> NoticeTagRelationship { get; set; }
        public DbSet<UserNoticeBoardFollow> UserBoardFollowingRelationship { get; set; }
        public DbSet<UserNoticePost> UserNoticePostRelationship { get; set; }
    }
}
