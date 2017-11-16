namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messsages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        Added = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.FromUserId, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.ToUserId, cascadeDelete: false)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "ToUserId", "dbo.User");
            DropForeignKey("dbo.Message", "FromUserId", "dbo.User");
            DropIndex("dbo.Message", new[] { "ToUserId" });
            DropIndex("dbo.Message", new[] { "FromUserId" });
            DropTable("dbo.Message");
        }
    }
}
