namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarketMaxContractsList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonteCarloSimulations", "MarketMaxContractsJson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MonteCarloSimulations", "MarketMaxContractsJson");
        }
    }
}
