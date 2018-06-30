namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedResultDatatypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "Download", c => c.Single(nullable: false));
            AlterColumn("dbo.Results", "Upload", c => c.Single(nullable: false));
            AlterColumn("dbo.Results", "Ping", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "Ping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Results", "Upload", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Results", "Download", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
