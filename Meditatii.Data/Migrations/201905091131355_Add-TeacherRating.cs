namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherRating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppoitmentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appoitment", t => t.AppoitmentId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.StudentId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.AppoitmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.StudentId);
            
            AddColumn("dbo.User", "Rating", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherRating", "TeacherId", "dbo.User");
            DropForeignKey("dbo.TeacherRating", "StudentId", "dbo.User");
            DropForeignKey("dbo.TeacherRating", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.TeacherRating", new[] { "StudentId" });
            DropIndex("dbo.TeacherRating", new[] { "TeacherId" });
            DropIndex("dbo.TeacherRating", new[] { "AppoitmentId" });
            DropColumn("dbo.User", "Rating");
            DropTable("dbo.TeacherRating");
        }
    }
}
