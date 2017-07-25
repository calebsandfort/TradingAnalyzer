namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTradingDayToTrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trades", "TradingDayId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trades", "TradingDayId");
            AddForeignKey("dbo.Trades", "TradingDayId", "dbo.TradingDays", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trades", "TradingDayId", "dbo.TradingDays");
            DropIndex("dbo.Trades", new[] { "TradingDayId" });
            DropColumn("dbo.Trades", "TradingDayId");
        }
    }
}
