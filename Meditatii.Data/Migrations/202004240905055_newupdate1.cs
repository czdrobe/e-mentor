namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupdate1 : DbMigration
    {
        public override void Up()
        {
            /*DropForeignKey("dbo.Payment", "Appoitment_Id", "dbo.Appoitment");
            DropIndex("dbo.Payment", new[] { "Appoitment_Id" });
            CreateIndex("dbo.Appoitment", "PaymentId");
            AddForeignKey("dbo.Appoitment", "PaymentId", "dbo.Payment", "Id");
            DropColumn("dbo.Payment", "Appoitment_Id");*/
        }
        
        public override void Down()
        {
           /*AddColumn("dbo.Payment", "Appoitment_Id", c => c.Int());
            DropForeignKey("dbo.Appoitment", "PaymentId", "dbo.Payment");
            DropIndex("dbo.Appoitment", new[] { "PaymentId" });
            CreateIndex("dbo.Payment", "Appoitment_Id");
            AddForeignKey("dbo.Payment", "Appoitment_Id", "dbo.Appoitment", "Id");*/
        }
    }
}
