namespace lunch.Repositories.Impl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.String(nullable: false, maxLength: 256),
                        Type = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 254),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        DisplayName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 1024),
                        PictureUrl = c.String(maxLength: 2048),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        Token = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Token)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSessions", "UserId", "dbo.Users");
            DropIndex("dbo.UserSessions", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.UserSessions");
            DropTable("dbo.Users");
        }
    }
}
