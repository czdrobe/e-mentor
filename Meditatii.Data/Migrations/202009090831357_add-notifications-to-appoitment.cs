namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotificationstoappoitment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoitment", "NotificationNew", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Appoitment", "NotificationAccepted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Appoitment", "NotificationPaid", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Appoitment", "NotificationRemainder", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appoitment", "NotificationRemainder");
            DropColumn("dbo.Appoitment", "NotificationPaid");
            DropColumn("dbo.Appoitment", "NotificationAccepted");
            DropColumn("dbo.Appoitment", "NotificationNew");
        }
    }
}
