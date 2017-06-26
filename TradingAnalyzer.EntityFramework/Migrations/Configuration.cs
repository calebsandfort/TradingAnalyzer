using System.Data.Entity.Migrations;

namespace TradingAnalyzer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TradingAnalyzer.EntityFramework.TradingAnalyzerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TradingAnalyzer";
        }

        protected override void Seed(TradingAnalyzer.EntityFramework.TradingAnalyzerDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
