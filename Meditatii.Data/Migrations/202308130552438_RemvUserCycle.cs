namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemvUserCycle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCycle", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCycle", "CycleId", "dbo.Cycle");
            DropIndex("dbo.UserCycle", new[] { "UserId" });
            DropIndex("dbo.UserCycle", new[] { "CycleId" });
            DropTable("dbo.UserCycle");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserCycle",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CycleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CycleId });
            
            CreateIndex("dbo.UserCycle", "CycleId");
            CreateIndex("dbo.UserCycle", "UserId");
            AddForeignKey("dbo.UserCycle", "CycleId", "dbo.Cycle", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserCycle", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
