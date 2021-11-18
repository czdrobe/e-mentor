namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupdate : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Appoitment", "PaymentId", c => c.Int());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Appoitment", "PaymentId", c => c.Int(nullable: false));
        }
    }
}
