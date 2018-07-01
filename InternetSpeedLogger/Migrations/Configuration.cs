namespace InternetSpeedLogger.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InternetSpeedLogger.Database.SqlServerDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InternetSpeedLogger.Database.SqlServerDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            
            //Execute ReportsView.sql to generate a view that displays the most recent test results in mb/s.
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var baseDir = Path.GetDirectoryName(path) + "\\Migrations\\ReportsView.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir));
        }
    }
}
