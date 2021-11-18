namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtousersnrofviewsandphoneviews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "NrOfVisitors", c => c.Int(nullable: false, defaultValue:0));
            AddColumn("dbo.User", "NrOfPhoneViews", c => c.Int(nullable: false, defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "NrOfPhoneViews");
            DropColumn("dbo.User", "NrOfVisitors");
        }
    }
}
