namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtousersaddedfiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Added", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Added");
        }
    }
}
