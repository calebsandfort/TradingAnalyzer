namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScreenshotRelationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trades", "EntryScreenshotDbId", c => c.Int());
            AddColumn("dbo.Trades", "ExitScreenshotDbId", c => c.Int());
            CreateIndex("dbo.Trades", "EntryScreenshotDbId");
            CreateIndex("dbo.Trades", "ExitScreenshotDbId");
            AddForeignKey("dbo.Trades", "EntryScreenshotDbId", "dbo.Screenshots", "Id");
            AddForeignKey("dbo.Trades", "ExitScreenshotDbId", "dbo.Screenshots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trades", "ExitScreenshotDbId", "dbo.Screenshots");
            DropForeignKey("dbo.Trades", "EntryScreenshotDbId", "dbo.Screenshots");
            DropIndex("dbo.Trades", new[] { "ExitScreenshotDbId" });
            DropIndex("dbo.Trades", new[] { "EntryScreenshotDbId" });
            DropColumn("dbo.Trades", "ExitScreenshotDbId");
            DropColumn("dbo.Trades", "EntryScreenshotDbId");
        }
    }
}
