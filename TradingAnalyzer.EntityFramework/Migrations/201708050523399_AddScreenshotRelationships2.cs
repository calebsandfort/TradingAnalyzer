namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScreenshotRelationships2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketLogEntries", "ScreenshotDbId", c => c.Int());
            CreateIndex("dbo.MarketLogEntries", "ScreenshotDbId");
            AddForeignKey("dbo.MarketLogEntries", "ScreenshotDbId", "dbo.Screenshots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MarketLogEntries", "ScreenshotDbId", "dbo.Screenshots");
            DropIndex("dbo.MarketLogEntries", new[] { "ScreenshotDbId" });
            DropColumn("dbo.MarketLogEntries", "ScreenshotDbId");
        }
    }
}
