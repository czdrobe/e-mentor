namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addservicefeeandtax : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ServiceFee", c => c.Int(nullable: false, defaultValue: 9));
            AddColumn("dbo.User", "Tax", c => c.Int(nullable: false, defaultValue: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Tax");
            DropColumn("dbo.User", "ServiceFee");
        }
    }
}
