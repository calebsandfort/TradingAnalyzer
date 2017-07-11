namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradingAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        InitialCapital = c.Double(nullable: false),
                        CurrentCapital = c.Double(nullable: false),
                        MiscFees = c.Double(nullable: false),
                        CommissionsAndFees = c.Double(nullable: false),
                        GrossPandL = c.Double(nullable: false),
                        NetPandL = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Trades", "Opened", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trades", "Closed", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trades", "RefNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "TradeType", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Trades", "MiscFees", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "CommissionsAndFees", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "GrossPandL", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "NetPandL", c => c.Double(nullable: false));
            AddColumn("dbo.Trades", "TradingAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trades", "TradingAccountId");
            AddForeignKey("dbo.Trades", "TradingAccountId", "dbo.TradingAccounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trades", "TradingAccountId", "dbo.TradingAccounts");
            DropIndex("dbo.Trades", new[] { "TradingAccountId" });
            DropColumn("dbo.Trades", "TradingAccountId");
            DropColumn("dbo.Trades", "NetPandL");
            DropColumn("dbo.Trades", "GrossPandL");
            DropColumn("dbo.Trades", "CommissionsAndFees");
            DropColumn("dbo.Trades", "MiscFees");
            DropColumn("dbo.Trades", "Quantity");
            DropColumn("dbo.Trades", "TradeType");
            DropColumn("dbo.Trades", "RefNumber");
            DropColumn("dbo.Trades", "Closed");
            DropColumn("dbo.Trades", "Opened");
            DropTable("dbo.TradingAccounts");
        }
    }
}
