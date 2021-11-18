namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FullName = c.String(),
                        County = c.String(),
                        Zip = c.String(),
                        Population = c.Int(nullable: false),
                        Lat = c.String(),
                        Lng = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CityUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.User", "AlsoOnline", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CityUser", "UserId", "dbo.User");
            DropForeignKey("dbo.CityUser", "CityId", "dbo.City");
            DropIndex("dbo.CityUser", new[] { "UserId" });
            DropIndex("dbo.CityUser", new[] { "CityId" });
            DropColumn("dbo.User", "AlsoOnline");
            DropTable("dbo.CityUser");
            DropTable("dbo.City");
        }
    }
}
