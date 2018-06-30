namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuildSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Ip = c.String(),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Isp = c.String(),
                        IspRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IspDlAvg = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IspUlAvg = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoggedIn = c.Int(nullable: false),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        Download = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Upload = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Timestamp = c.DateTime(nullable: false),
                        BytesSent = c.Int(nullable: false),
                        BytesReceived = c.Int(nullable: false),
                        Share = c.String(),
                        Client_ClientId = c.Int(),
                        Server_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .ForeignKey("dbo.Servers", t => t.Server_Id)
                .Index(t => t.Client_ClientId)
                .Index(t => t.Server_Id);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ServerId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        Country = c.String(),
                        CC = c.String(),
                        Sponsor = c.String(),
                        Host = c.String(),
                        D = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latency = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "Server_Id", "dbo.Servers");
            DropForeignKey("dbo.Results", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Results", new[] { "Server_Id" });
            DropIndex("dbo.Results", new[] { "Client_ClientId" });
            DropTable("dbo.Servers");
            DropTable("dbo.Results");
            DropTable("dbo.Clients");
        }
    }
}
