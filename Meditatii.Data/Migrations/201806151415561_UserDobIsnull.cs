namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDobIsnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Dob", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Dob", c => c.DateTime(nullable: false));
        }
    }
}
