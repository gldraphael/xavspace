namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsArchivedfieldadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoticeBoards", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoticeBoards", "IsArchived");
        }
    }
}
