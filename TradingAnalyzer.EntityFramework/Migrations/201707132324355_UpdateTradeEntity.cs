namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTradeEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trades", "MarketId", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "Timeframe", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "EntryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trades", "EntryPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "EntrySetups", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "EntryRemarks", c => c.String());
            AddColumn("dbo.Trades", "EntryScreenshot", c => c.String());
            AddColumn("dbo.Trades", "MFE", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "MFA", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "ExitDate", c => c.DateTime());
            AddColumn("dbo.Trades", "ExitPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "ExitReason", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "ExitRemarks", c => c.String());
            AddColumn("dbo.Trades", "ExitScreenshot", c => c.String());
            AddColumn("dbo.Trades", "Commissions", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "Classification", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "ProfitLoss", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "ProfitLossPerContract", c => c.Double(nullable: false));
            CreateIndex("dbo.Trades", "MarketId");
            AddForeignKey("dbo.Trades", "MarketId", "dbo.Markets", "Id", cascadeDelete: true);
            DropColumn("dbo.Trades", "Opened");
            DropColumn("dbo.Trades", "Closed");
            DropColumn("dbo.Trades", "Quantity");
            DropColumn("dbo.Trades", "MiscFees");
            DropColumn("dbo.Trades", "CommissionsAndFees");
            DropColumn("dbo.Trades", "GrossPandL");
            DropColumn("dbo.Trades", "NetPandL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trades", "NetPandL", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "GrossPandL", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "CommissionsAndFees", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "MiscFees", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "Closed", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trades", "Opened", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Trades", "MarketId", "dbo.Markets");
            DropIndex("dbo.Trades", new[] { "MarketId" });
            DropColumn("dbo.Trades", "ProfitLossPerContract");
            DropColumn("dbo.Trades", "Size");
            DropColumn("dbo.Trades", "ProfitLoss");
            DropColumn("dbo.Trades", "Classification");
            DropColumn("dbo.Trades", "Commissions");
            DropColumn("dbo.Trades", "ExitScreenshot");
            DropColumn("dbo.Trades", "ExitRemarks");
            DropColumn("dbo.Trades", "ExitReason");
            DropColumn("dbo.Trades", "ExitPrice");
            DropColumn("dbo.Trades", "ExitDate");
            DropColumn("dbo.Trades", "MFA");
            DropColumn("dbo.Trades", "MFE");
            DropColumn("dbo.Trades", "EntryScreenshot");
            DropColumn("dbo.Trades", "EntryRemarks");
            DropColumn("dbo.Trades", "EntrySetups");
            DropColumn("dbo.Trades", "EntryPrice");
            DropColumn("dbo.Trades", "EntryDate");
            DropColumn("dbo.Trades", "Timeframe");
            DropColumn("dbo.Trades", "MarketId");
        }
    }
}
