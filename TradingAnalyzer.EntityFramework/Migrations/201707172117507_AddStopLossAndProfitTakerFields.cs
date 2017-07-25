namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStopLossAndProfitTakerFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trades", "StopLossPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Trades", "ProfitTakerPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Trades", "MFE");
            DropColumn("dbo.Trades", "MFA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trades", "MFA", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Trades", "MFE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Trades", "ProfitTakerPrice");
            DropColumn("dbo.Trades", "StopLossPrice");
        }
    }
}
