namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppoitmentId = c.Int(nullable: false),
                        LearnerId = c.Int(nullable: false),
                        Added = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AppoitmentId)
                .Index(t => t.LearnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "LearnerId", "dbo.User");
            DropForeignKey("dbo.Payment", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.Payment", new[] { "LearnerId" });
            DropIndex("dbo.Payment", new[] { "AppoitmentId" });
            DropTable("dbo.Payment");
        }
    }
}
