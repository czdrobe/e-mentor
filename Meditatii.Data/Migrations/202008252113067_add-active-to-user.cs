namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addactivetouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Active", c => c.Boolean(false, false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Active");
        }
    }
}
