namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarketToMarketLogEntry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketLogEntries", "MarketId", c => c.Int(nullable: false));
            CreateIndex("dbo.MarketLogEntries", "MarketId");
            AddForeignKey("dbo.MarketLogEntries", "MarketId", "dbo.Markets", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MarketLogEntries", "MarketId", "dbo.Markets");
            DropIndex("dbo.MarketLogEntries", new[] { "MarketId" });
            DropColumn("dbo.MarketLogEntries", "MarketId");
        }
    }
}
