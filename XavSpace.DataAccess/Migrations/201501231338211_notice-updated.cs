namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noticeupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notices", "DateReviewed", c => c.DateTime());
            AddColumn("dbo.Notices", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Notices", "DateApproved");
            DropColumn("dbo.Notices", "IsApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notices", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notices", "DateApproved", c => c.DateTime());
            DropColumn("dbo.Notices", "Status");
            DropColumn("dbo.Notices", "DateReviewed");
        }
    }
}
