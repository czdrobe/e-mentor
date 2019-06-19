namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacheravaibilitynew : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.TeacherAvailability",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            */
        }
        
        public override void Down()
        {
            /*
            DropForeignKey("dbo.TeacherAvailability", "TeacherId", "dbo.User");
            DropIndex("dbo.TeacherAvailability", new[] { "TeacherId" });
            DropTable("dbo.TeacherAvailability");
            */
        }
    }
}
