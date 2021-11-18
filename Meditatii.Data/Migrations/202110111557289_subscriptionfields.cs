namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subscriptionfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "SubscriptionStartDate", c => c.DateTime());
            AddColumn("dbo.User", "SubscriptionEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "SubscriptionEndDate");
            DropColumn("dbo.User", "SubscriptionStartDate");
        }
    }
}
