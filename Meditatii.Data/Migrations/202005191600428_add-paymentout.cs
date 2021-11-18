namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpaymentout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentOut",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppoitmentId = c.Int(nullable: false),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appoitment", t => t.AppoitmentId, cascadeDelete: true)
                .Index(t => t.AppoitmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentOut", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.PaymentOut", new[] { "AppoitmentId" });
            DropTable("dbo.PaymentOut");
        }
    }
}
