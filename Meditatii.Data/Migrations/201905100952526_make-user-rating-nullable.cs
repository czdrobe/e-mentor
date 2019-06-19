namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeuserratingnullable : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.User", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.User", "Rating", c => c.Int(nullable: false));
        }
    }
}
