namespace TaskManagement.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordhash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "PasswordHash", c => c.String());
            DropColumn("dbo.Accounts", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Password", c => c.String());
            DropColumn("dbo.Accounts", "PasswordHash");
        }
    }
}
