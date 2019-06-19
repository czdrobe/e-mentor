namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpriceappoitment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoitment", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appoitment", "Price");
        }
    }
}
