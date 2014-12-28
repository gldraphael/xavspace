namespace XavSpace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MiddleNameRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Name_Middle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Name_Middle", c => c.String());
        }
    }
}
