namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AdCycle",
                c => new
                    {
                        AdId = c.Int(nullable: false),
                        CycleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AdId, t.CycleId })
                .ForeignKey("dbo.Ad", t => t.AdId, cascadeDelete: true)
                .ForeignKey("dbo.Cycle", t => t.CycleId, cascadeDelete: true)
                .Index(t => t.AdId)
                .Index(t => t.CycleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ad", "TeacherId", "dbo.User");
            DropForeignKey("dbo.AdCycle", "CycleId", "dbo.Cycle");
            DropForeignKey("dbo.AdCycle", "AdId", "dbo.Ad");
            DropForeignKey("dbo.Ad", "CategoryId", "dbo.Category");
            DropIndex("dbo.AdCycle", new[] { "CycleId" });
            DropIndex("dbo.AdCycle", new[] { "AdId" });
            DropIndex("dbo.Ad", new[] { "CategoryId" });
            DropIndex("dbo.Ad", new[] { "TeacherId" });
            DropTable("dbo.AdCycle");
            DropTable("dbo.Ad");
        }
    }
}
