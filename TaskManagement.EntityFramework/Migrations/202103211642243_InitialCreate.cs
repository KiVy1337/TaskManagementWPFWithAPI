namespace TaskManagement.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        DatesJoined = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Progress = c.Int(nullable: false),
                        IssueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.Issues", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Tasks", new[] { "IssueId" });
            DropIndex("dbo.Issues", new[] { "AccountId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Issues");
            DropTable("dbo.Accounts");
        }
    }
}
