namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experience",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Occupation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "AtTeacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "AtStudent", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "YouYubeURL", c => c.String());
            AddColumn("dbo.User", "OccupationId", c => c.Int(nullable: false));
            AddColumn("dbo.User", "ExperienceId", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Studies", c => c.String());
            CreateIndex("dbo.User", "OccupationId");
            CreateIndex("dbo.User", "ExperienceId");
            AddForeignKey("dbo.User", "ExperienceId", "dbo.Experience", "Id", cascadeDelete: true);
            AddForeignKey("dbo.User", "OccupationId", "dbo.Occupation", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "OccupationId", "dbo.Occupation");
            DropForeignKey("dbo.User", "ExperienceId", "dbo.Experience");
            DropIndex("dbo.User", new[] { "ExperienceId" });
            DropIndex("dbo.User", new[] { "OccupationId" });
            DropColumn("dbo.User", "Studies");
            DropColumn("dbo.User", "ExperienceId");
            DropColumn("dbo.User", "OccupationId");
            DropColumn("dbo.User", "YouYubeURL");
            DropColumn("dbo.User", "AtStudent");
            DropColumn("dbo.User", "AtTeacher");
            DropTable("dbo.Occupation");
            DropTable("dbo.Experience");
        }
    }
}
