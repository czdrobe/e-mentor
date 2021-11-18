namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproducttopaymen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "Product", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "Product");
        }
    }
}
