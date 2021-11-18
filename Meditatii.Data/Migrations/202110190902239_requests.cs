namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LearnerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        IsOnline = c.Boolean(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LearnerId, cascadeDelete: true)
                .Index(t => t.LearnerId)
                .Index(t => t.CategoryId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "LearnerId", "dbo.User");
            DropForeignKey("dbo.Request", "CityId", "dbo.City");
            DropForeignKey("dbo.Request", "CategoryId", "dbo.Category");
            DropIndex("dbo.Request", new[] { "CityId" });
            DropIndex("dbo.Request", new[] { "CategoryId" });
            DropIndex("dbo.Request", new[] { "LearnerId" });
            DropTable("dbo.Request");
        }
    }
}
