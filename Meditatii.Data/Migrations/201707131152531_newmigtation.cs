namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigtation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Sex = c.Boolean(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        County = c.String(),
                        City = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cycle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCategory",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CategoryId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.UserCycle",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CycleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CycleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Cycle", t => t.CycleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CycleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCycle", "CycleId", "dbo.Cycle");
            DropForeignKey("dbo.UserCycle", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.UserCategory", "UserId", "dbo.User");
            DropIndex("dbo.UserCycle", new[] { "CycleId" });
            DropIndex("dbo.UserCycle", new[] { "UserId" });
            DropIndex("dbo.UserCategory", new[] { "CategoryId" });
            DropIndex("dbo.UserCategory", new[] { "UserId" });
            DropTable("dbo.UserCycle");
            DropTable("dbo.UserCategory");
            DropTable("dbo.Cycle");
            DropTable("dbo.User");
            DropTable("dbo.Category");
        }
    }
}
