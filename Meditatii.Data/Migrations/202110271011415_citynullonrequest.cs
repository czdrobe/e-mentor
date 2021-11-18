namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class citynullonrequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Request", "CityId", "dbo.City");
            DropIndex("dbo.Request", new[] { "CityId" });
            AlterColumn("dbo.Request", "CityId", c => c.Int());
            CreateIndex("dbo.Request", "CityId");
            AddForeignKey("dbo.Request", "CityId", "dbo.City", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "CityId", "dbo.City");
            DropIndex("dbo.Request", new[] { "CityId" });
            AlterColumn("dbo.Request", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Request", "CityId");
            AddForeignKey("dbo.Request", "CityId", "dbo.City", "Id", cascadeDelete: true);
        }
    }
}
