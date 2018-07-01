namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Results", "Server_Id", "dbo.Servers");
            DropIndex("dbo.Results", new[] { "Client_ClientId" });
            DropIndex("dbo.Results", new[] { "Server_Id" });
            RenameColumn(table: "dbo.Results", name: "Client_ClientId", newName: "ClientId");
            RenameColumn(table: "dbo.Results", name: "Server_Id", newName: "ServerId");
            AlterColumn("dbo.Results", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.Results", "ServerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Results", "ServerId");
            CreateIndex("dbo.Results", "ClientId");
            AddForeignKey("dbo.Results", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
            AddForeignKey("dbo.Results", "ServerId", "dbo.Servers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Results", "ClientId", "dbo.Clients");
            DropIndex("dbo.Results", new[] { "ClientId" });
            DropIndex("dbo.Results", new[] { "ServerId" });
            AlterColumn("dbo.Results", "ServerId", c => c.Int());
            AlterColumn("dbo.Results", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.Results", name: "ServerId", newName: "Server_Id");
            RenameColumn(table: "dbo.Results", name: "ClientId", newName: "Client_ClientId");
            CreateIndex("dbo.Results", "Server_Id");
            CreateIndex("dbo.Results", "Client_ClientId");
            AddForeignKey("dbo.Results", "Server_Id", "dbo.Servers", "Id");
            AddForeignKey("dbo.Results", "Client_ClientId", "dbo.Clients", "ClientId");
        }
    }
}
