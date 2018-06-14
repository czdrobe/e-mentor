namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appoitments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appoitment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        LearnerId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.LearnerId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.LearnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appoitment", "TeacherId", "dbo.User");
            DropForeignKey("dbo.Appoitment", "LearnerId", "dbo.User");
            DropIndex("dbo.Appoitment", new[] { "LearnerId" });
            DropIndex("dbo.Appoitment", new[] { "TeacherId" });
            DropTable("dbo.Appoitment");
        }
    }
}
