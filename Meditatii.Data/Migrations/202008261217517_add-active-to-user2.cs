namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addactivetouser2 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.User", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.User", "Active");
        }
    }
}
