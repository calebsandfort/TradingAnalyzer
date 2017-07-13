namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarketLogEntryType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketLogEntries", "MarketLogEntryType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MarketLogEntries", "MarketLogEntryType");
        }
    }
}
