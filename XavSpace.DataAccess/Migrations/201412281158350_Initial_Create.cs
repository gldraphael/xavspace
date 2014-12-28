namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoticeBoards",
                c => new
                    {
                        NoticeBoardId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsOfficial = c.Boolean(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.NoticeBoardId);
            
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        NoticeId = c.Int(nullable: false, identity: true),
                        NoticeBoardId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateApproved = c.DateTime(),
                        HighPriority = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NoticeId)
                .ForeignKey("dbo.NoticeBoards", t => t.NoticeBoardId, cascadeDelete: true)
                .Index(t => t.NoticeBoardId);
            
            CreateTable(
                "dbo.NoticeTags",
                c => new
                    {
                        NoticeId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoticeId, t.TagId })
                .ForeignKey("dbo.Notices", t => t.NoticeId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.NoticeId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserNoticeBoardFollows",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        NoticeBoardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NoticeBoardId })
                .ForeignKey("dbo.NoticeBoards", t => t.NoticeBoardId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NoticeBoardId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name_First = c.String(),
                        Name_Middle = c.String(),
                        Name_Last = c.String(),
                        Gender = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Post = c.String(),
                        Year = c.Int(),
                        Class = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserNoticePosts",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        NoticeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NoticeId })
                .ForeignKey("dbo.Notices", t => t.NoticeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NoticeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNoticePosts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNoticePosts", "NoticeId", "dbo.Notices");
            DropForeignKey("dbo.UserNoticeBoardFollows", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNoticeBoardFollows", "NoticeBoardId", "dbo.NoticeBoards");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.NoticeTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.NoticeTags", "NoticeId", "dbo.Notices");
            DropForeignKey("dbo.Notices", "NoticeBoardId", "dbo.NoticeBoards");
            DropIndex("dbo.UserNoticePosts", new[] { "NoticeId" });
            DropIndex("dbo.UserNoticePosts", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserNoticeBoardFollows", new[] { "NoticeBoardId" });
            DropIndex("dbo.UserNoticeBoardFollows", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NoticeTags", new[] { "TagId" });
            DropIndex("dbo.NoticeTags", new[] { "NoticeId" });
            DropIndex("dbo.Notices", new[] { "NoticeBoardId" });
            DropTable("dbo.UserNoticePosts");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserNoticeBoardFollows");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.NoticeTags");
            DropTable("dbo.Notices");
            DropTable("dbo.NoticeBoards");
        }
    }
}
