namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Price");
        }
    }
}
