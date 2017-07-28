namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreMonteCarloFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonteCarloSimulations", "CumulativeProfitK", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "CumulativeProfit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "TradingEdge", c => c.Boolean(nullable: false));
            AddColumn("dbo.MonteCarloSimulations", "ConsecutiveLossesK", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "ConsecutiveLosses", c => c.Int(nullable: false));
            AddColumn("dbo.MonteCarloSimulations", "MaxDrawdownK", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "MaxDrawdown", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "AccountSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "RuinPoint", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "MaxDrawdownMultiple", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "OneContractFunds", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MonteCarloSimulations", "MaxContracts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MonteCarloSimulations", "MaxContracts");
            DropColumn("dbo.MonteCarloSimulations", "OneContractFunds");
            DropColumn("dbo.MonteCarloSimulations", "MaxDrawdownMultiple");
            DropColumn("dbo.MonteCarloSimulations", "RuinPoint");
            DropColumn("dbo.MonteCarloSimulations", "AccountSize");
            DropColumn("dbo.MonteCarloSimulations", "MaxDrawdown");
            DropColumn("dbo.MonteCarloSimulations", "MaxDrawdownK");
            DropColumn("dbo.MonteCarloSimulations", "ConsecutiveLosses");
            DropColumn("dbo.MonteCarloSimulations", "ConsecutiveLossesK");
            DropColumn("dbo.MonteCarloSimulations", "TradingEdge");
            DropColumn("dbo.MonteCarloSimulations", "CumulativeProfit");
            DropColumn("dbo.MonteCarloSimulations", "CumulativeProfitK");
        }
    }
}
