namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarketLogEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Text = c.String(),
                        Screenshot = c.String(),
                        MarketLogEntryType = c.Int(nullable: false),
                        TradingDayId = c.Int(nullable: false),
                        TradingAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TradingAccounts", t => t.TradingAccountId, cascadeDelete: true)
                .ForeignKey("dbo.TradingDays", t => t.TradingDayId, cascadeDelete: true)
                .Index(t => t.TradingDayId)
                .Index(t => t.TradingAccountId);
            
            CreateTable(
                "dbo.TradingAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        InitialCapital = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentCapital = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Commissions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProfitLoss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefNumber = c.Int(nullable: false),
                        MarketId = c.Int(nullable: false),
                        Timeframe = c.Int(nullable: false),
                        TradeType = c.Int(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        EntryPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EntrySetups = c.Int(nullable: false),
                        EntryRemarks = c.String(),
                        EntryScreenshot = c.String(),
                        MFE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MFA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExitDate = c.DateTime(),
                        ExitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExitReason = c.Int(nullable: false),
                        ExitRemarks = c.String(),
                        ExitScreenshot = c.String(),
                        Commissions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Classification = c.Int(nullable: false),
                        ProfitLoss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Size = c.Int(nullable: false),
                        ProfitLossPerContract = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TradingAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Markets", t => t.MarketId, cascadeDelete: true)
                .ForeignKey("dbo.TradingAccounts", t => t.TradingAccountId, cascadeDelete: true)
                .Index(t => t.MarketId)
                .Index(t => t.TradingAccountId);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Symbol = c.String(),
                        TickValue = c.Double(nullable: false),
                        TickSize = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TradingDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TradingDirectives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        TradingDirectiveType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MarketLogEntries", "TradingDayId", "dbo.TradingDays");
            DropForeignKey("dbo.Trades", "TradingAccountId", "dbo.TradingAccounts");
            DropForeignKey("dbo.Trades", "MarketId", "dbo.Markets");
            DropForeignKey("dbo.MarketLogEntries", "TradingAccountId", "dbo.TradingAccounts");
            DropIndex("dbo.Trades", new[] { "TradingAccountId" });
            DropIndex("dbo.Trades", new[] { "MarketId" });
            DropIndex("dbo.MarketLogEntries", new[] { "TradingAccountId" });
            DropIndex("dbo.MarketLogEntries", new[] { "TradingDayId" });
            DropTable("dbo.TradingDirectives");
            DropTable("dbo.TradingDays");
            DropTable("dbo.Markets");
            DropTable("dbo.Trades");
            DropTable("dbo.TradingAccounts");
            DropTable("dbo.MarketLogEntries");
        }
    }
}
