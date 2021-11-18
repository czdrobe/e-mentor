namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentIdToAppoitment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payment", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.Payment", new[] { "AppoitmentId" });
            DropColumn(table: "dbo.Payment", name: "AppoitmentId");
            AddColumn("dbo.Appoitment", "PaymentId", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appoitment", "PaymentId");
            AddColumn("dbo.Payment", "AppoitmentId", c => c.Int(nullable: true));
            CreateIndex("dbo.Payment", "AppoitmentId");
            AddForeignKey("dbo.Payment", "AppoitmentId", "dbo.Appoitment", "Id", cascadeDelete: true);
        }
    }
}
