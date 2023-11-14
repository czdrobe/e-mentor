namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ad", "Added", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ad", "Added");
        }
    }
}
