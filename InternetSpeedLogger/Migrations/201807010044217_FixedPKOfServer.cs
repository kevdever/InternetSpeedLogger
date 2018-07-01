namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedPKOfServer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Server_Id", "dbo.Servers");
            DropPrimaryKey("dbo.Servers");
            AlterColumn("dbo.Servers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Servers", "Id");
            AddForeignKey("dbo.Results", "Server_Id", "dbo.Servers", "Id");
            DropColumn("dbo.Servers", "ServerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servers", "ServerId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Results", "Server_Id", "dbo.Servers");
            DropPrimaryKey("dbo.Servers");
            AlterColumn("dbo.Servers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Servers", "Id");
            AddForeignKey("dbo.Results", "Server_Id", "dbo.Servers", "Id");
        }
    }
}
