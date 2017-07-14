namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTradingAccountEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketLogEntries", "TradingAccountId", c => c.Int(nullable: false));
            AddColumn("dbo.TradingAccounts", "Commissions", c => c.Double(nullable: false));
            AddColumn("dbo.TradingAccounts", "ProfitLoss", c => c.Double(nullable: false));
            AddColumn("dbo.TradingAccounts", "Active", c => c.Boolean(nullable: false));
            CreateIndex("dbo.MarketLogEntries", "TradingAccountId");
            AddForeignKey("dbo.MarketLogEntries", "TradingAccountId", "dbo.TradingAccounts", "Id", cascadeDelete: true);
            DropColumn("dbo.TradingAccounts", "MiscFees");
            DropColumn("dbo.TradingAccounts", "CommissionsAndFees");
            DropColumn("dbo.TradingAccounts", "GrossPandL");
            DropColumn("dbo.TradingAccounts", "NetPandL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TradingAccounts", "NetPandL", c => c.Double(nullable: false));
            AddColumn("dbo.TradingAccounts", "GrossPandL", c => c.Double(nullable: false));
            AddColumn("dbo.TradingAccounts", "CommissionsAndFees", c => c.Double(nullable: false));
            AddColumn("dbo.TradingAccounts", "MiscFees", c => c.Double(nullable: false));
            DropForeignKey("dbo.MarketLogEntries", "TradingAccountId", "dbo.TradingAccounts");
            DropIndex("dbo.MarketLogEntries", new[] { "TradingAccountId" });
            DropColumn("dbo.TradingAccounts", "Active");
            DropColumn("dbo.TradingAccounts", "ProfitLoss");
            DropColumn("dbo.TradingAccounts", "Commissions");
            DropColumn("dbo.MarketLogEntries", "TradingAccountId");
        }
    }
}
