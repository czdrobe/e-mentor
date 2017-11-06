namespace meditatii.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityupdate : DbMigration
    {
        public override void Up()
        {
            /* RenameTable(name: "dbo.AspNetRoles", newName: "Roles");
             RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRoles");
             RenameTable(name: "dbo.AspNetUsers", newName: "User");
             RenameTable(name: "dbo.AspNetUserClaims", newName: "UserClaim");
             RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogin");
             DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
             DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
             DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
             DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers")
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropPrimaryKey("dbo.Roles");
            DropPrimaryKey("dbo.UserRoles");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserLogin");;*/
            AlterColumn("dbo.Roles", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserRoles", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRoles", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserClaims", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserLogins", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Roles", "Id");
            AddPrimaryKey("dbo.UserRoles", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.UserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("dbo.UserRoles", "UserId");
            CreateIndex("dbo.UserRoles", "RoleId");
            CreateIndex("dbo.UserClaims", "UserId");
            CreateIndex("dbo.UserLogins", "UserId");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLogins", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropPrimaryKey("dbo.UserLogin");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserRoles");
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.UserLogin", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserClaim", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.User", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRoles", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRoles", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Roles", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserLogin", new[] { "LoginProvider", "ProviderKey", "UserId" });
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.UserRoles", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.Roles", "Id");
            CreateIndex("dbo.UserLogin", "UserId");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.UserRoles", "RoleId");
            CreateIndex("dbo.UserRoles", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.UserLogin", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.UserClaim", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.User", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Roles", newName: "AspNetRoles");
        }
    }
}
