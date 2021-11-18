namespace Meditatii.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addacceptedbyteacher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoitment", "AcceptedByTeacher", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appoitment", "AcceptedByTeacher");
        }
    }
}
