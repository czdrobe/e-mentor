namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingtext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeacherRating", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.TeacherRating", new[] { "AppoitmentId" });
            AddColumn("dbo.TeacherRating", "RatingText", c => c.String());
            AlterColumn("dbo.TeacherRating", "AppoitmentId", c => c.Int());
            CreateIndex("dbo.TeacherRating", "AppoitmentId");
            AddForeignKey("dbo.TeacherRating", "AppoitmentId", "dbo.Appoitment", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherRating", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.TeacherRating", new[] { "AppoitmentId" });
            AlterColumn("dbo.TeacherRating", "AppoitmentId", c => c.Int(nullable: false));
            DropColumn("dbo.TeacherRating", "RatingText");
            CreateIndex("dbo.TeacherRating", "AppoitmentId");
            AddForeignKey("dbo.TeacherRating", "AppoitmentId", "dbo.Appoitment", "Id", cascadeDelete: true);
        }
    }
}
