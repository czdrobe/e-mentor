namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcycletorequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "CycleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Request", "CycleId");
            AddForeignKey("dbo.Request", "CycleId", "dbo.Cycle", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "CycleId", "dbo.Cycle");
            DropIndex("dbo.Request", new[] { "CycleId" });
            DropColumn("dbo.Request", "CycleId");
        }
    }
}
