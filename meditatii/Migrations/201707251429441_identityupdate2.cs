namespace meditatii.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityupdate2 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.UserClaim", newName: "UserClaims");
            //RenameTable(name: "dbo.UserLogin", newName: "UserLogins");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserLogins", newName: "UserLogin");
            RenameTable(name: "dbo.UserClaims", newName: "UserClaim");
        }
    }
}
