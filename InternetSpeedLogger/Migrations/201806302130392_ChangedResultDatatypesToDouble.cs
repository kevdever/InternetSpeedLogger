namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedResultDatatypesToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "Download", c => c.Double(nullable: false));
            AlterColumn("dbo.Results", "Upload", c => c.Double(nullable: false));
            AlterColumn("dbo.Results", "Ping", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "Ping", c => c.Single(nullable: false));
            AlterColumn("dbo.Results", "Upload", c => c.Single(nullable: false));
            AlterColumn("dbo.Results", "Download", c => c.Single(nullable: false));
        }
    }
}
