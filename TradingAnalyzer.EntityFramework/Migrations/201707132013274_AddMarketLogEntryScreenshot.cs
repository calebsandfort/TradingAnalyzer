namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarketLogEntryScreenshot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketLogEntries", "Screenshot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MarketLogEntries", "Screenshot");
        }
    }
}
