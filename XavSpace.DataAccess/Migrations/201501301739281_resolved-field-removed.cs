namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resolvedfieldremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ErrorLogs", "Resolved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ErrorLogs", "Resolved", c => c.Boolean(nullable: false));
        }
    }
}
