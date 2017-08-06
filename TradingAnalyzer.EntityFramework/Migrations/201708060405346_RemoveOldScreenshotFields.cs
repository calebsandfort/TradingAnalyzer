namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOldScreenshotFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MarketLogEntries", "Screenshot");
            DropColumn("dbo.Trades", "EntryScreenshot");
            DropColumn("dbo.Trades", "ExitScreenshot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trades", "ExitScreenshot", c => c.String());
            AddColumn("dbo.Trades", "EntryScreenshot", c => c.String());
            AddColumn("dbo.MarketLogEntries", "Screenshot", c => c.String());
        }
    }
}
