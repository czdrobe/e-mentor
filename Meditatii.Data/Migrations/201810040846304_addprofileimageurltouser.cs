namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprofileimageurltouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("User", "ProfileImageUrl", c => c.String(nullable: false, defaultValue: ""));
        }
        
        public override void Down()
        {
            DropColumn("User", "ProfileImageUrl");
        }
    }
}
