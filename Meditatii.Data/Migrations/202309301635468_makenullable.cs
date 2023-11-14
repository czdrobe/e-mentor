namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makenullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "ExperienceId", "dbo.Experience");
            DropForeignKey("dbo.User", "OccupationId", "dbo.Occupation");
            DropIndex("dbo.User", new[] { "OccupationId" });
            DropIndex("dbo.User", new[] { "ExperienceId" });
            AlterColumn("dbo.User", "OccupationId", c => c.Int());
            AlterColumn("dbo.User", "ExperienceId", c => c.Int());
            CreateIndex("dbo.User", "OccupationId");
            CreateIndex("dbo.User", "ExperienceId");
            AddForeignKey("dbo.User", "ExperienceId", "dbo.Experience", "Id");
            AddForeignKey("dbo.User", "OccupationId", "dbo.Occupation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "OccupationId", "dbo.Occupation");
            DropForeignKey("dbo.User", "ExperienceId", "dbo.Experience");
            DropIndex("dbo.User", new[] { "ExperienceId" });
            DropIndex("dbo.User", new[] { "OccupationId" });
            AlterColumn("dbo.User", "ExperienceId", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "OccupationId", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "ExperienceId");
            CreateIndex("dbo.User", "OccupationId");
            AddForeignKey("dbo.User", "OccupationId", "dbo.Occupation", "Id", cascadeDelete: true);
            AddForeignKey("dbo.User", "ExperienceId", "dbo.Experience", "Id", cascadeDelete: true);
        }
    }
}
