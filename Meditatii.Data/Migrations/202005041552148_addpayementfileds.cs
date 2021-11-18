namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpayementfileds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "PaymentTimeStamp", c => c.String());
            AddColumn("dbo.Payment", "PaymentCRC", c => c.String());
            AddColumn("dbo.Payment", "Updated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "Updated");
            DropColumn("dbo.Payment", "PaymentCRC");
            DropColumn("dbo.Payment", "PaymentTimeStamp");
        }
    }
}
