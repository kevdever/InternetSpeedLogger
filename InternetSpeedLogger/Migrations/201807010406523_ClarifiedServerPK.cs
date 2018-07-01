namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClarifiedServerPK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "ServerId", "dbo.Servers");
            DropPrimaryKey("dbo.Servers");
            AlterColumn("dbo.Servers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Servers", "Id");
            AddForeignKey("dbo.Results", "ServerId", "dbo.Servers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "ServerId", "dbo.Servers");
            DropPrimaryKey("dbo.Servers");
            AlterColumn("dbo.Servers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Servers", "Id");
            AddForeignKey("dbo.Results", "ServerId", "dbo.Servers", "Id", cascadeDelete: true);
        }
    }
}
