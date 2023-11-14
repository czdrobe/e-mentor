namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecatfromuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCategory", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCategory", "CategoryId", "dbo.Category");
            DropIndex("dbo.UserCategory", new[] { "UserId" });
            DropIndex("dbo.UserCategory", new[] { "CategoryId" });
            DropTable("dbo.UserCategory");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserCategory",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CategoryId });
            
            CreateIndex("dbo.UserCategory", "CategoryId");
            CreateIndex("dbo.UserCategory", "UserId");
            AddForeignKey("dbo.UserCategory", "CategoryId", "dbo.Category", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserCategory", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
