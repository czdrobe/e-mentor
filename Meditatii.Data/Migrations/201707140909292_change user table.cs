namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeusertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PhoneNumber", c => c.String());
            DropColumn("dbo.User", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Phone", c => c.String());
            DropColumn("dbo.User", "PhoneNumber");
        }
    }
}
