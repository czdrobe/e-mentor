namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appoitmentchattable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppoitmentChat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppoitmentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Message = c.String(),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appoitment", t => t.AppoitmentId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: false)
                .Index(t => t.AppoitmentId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppoitmentChat", "UserId", "dbo.User");
            DropForeignKey("dbo.AppoitmentChat", "AppoitmentId", "dbo.Appoitment");
            DropIndex("dbo.AppoitmentChat", new[] { "UserId" });
            DropIndex("dbo.AppoitmentChat", new[] { "AppoitmentId" });
            DropTable("dbo.AppoitmentChat");
        }
    }
}
