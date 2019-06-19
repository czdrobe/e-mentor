namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sessionidtoappoitment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoitment", "SessionId", c => c.String());
            AddColumn("dbo.Appoitment", "TokenId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appoitment", "TokenId");
            DropColumn("dbo.Appoitment", "SessionId");
        }
    }
}
