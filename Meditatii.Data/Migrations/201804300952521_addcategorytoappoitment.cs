namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategorytoappoitment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoitment", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appoitment", "CategoryId");
            AddForeignKey("dbo.Appoitment", "CategoryId", "dbo.Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appoitment", "CategoryId", "dbo.Category");
            DropIndex("dbo.Appoitment", new[] { "CategoryId" });
            DropColumn("dbo.Appoitment", "CategoryId");
        }
    }
}
