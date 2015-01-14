namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noticeupdatedandtagsremoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NoticeTags", "NoticeId", "dbo.Notices");
            DropForeignKey("dbo.NoticeTags", "TagId", "dbo.Tags");
            DropIndex("dbo.NoticeTags", new[] { "NoticeId" });
            DropIndex("dbo.NoticeTags", new[] { "TagId" });
            AddColumn("dbo.Notices", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notices", "ModeratorComment", c => c.String());
            DropTable("dbo.NoticeTags");
            DropTable("dbo.Tags");
        }
        
        public override void Down()
        {
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
                "dbo.NoticeTags",
                c => new
                    {
                        NoticeId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoticeId, t.TagId });
            
            DropColumn("dbo.Notices", "ModeratorComment");
            DropColumn("dbo.Notices", "IsApproved");
            CreateIndex("dbo.NoticeTags", "TagId");
            CreateIndex("dbo.NoticeTags", "NoticeId");
            AddForeignKey("dbo.NoticeTags", "TagId", "dbo.Tags", "TagId", cascadeDelete: true);
            AddForeignKey("dbo.NoticeTags", "NoticeId", "dbo.Notices", "NoticeId", cascadeDelete: true);
        }
    }
}
