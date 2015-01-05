namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UId");
        }
    }
}
