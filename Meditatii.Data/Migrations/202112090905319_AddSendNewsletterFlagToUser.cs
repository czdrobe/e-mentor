namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSendNewsletterFlagToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "SendNewsletter", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "SendNewsletter");
        }
    }
}
