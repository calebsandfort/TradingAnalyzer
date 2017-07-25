namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMonteCarloSimulation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonteCarloSimulations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        NumberOfTradesInSample = c.Int(nullable: false),
                        NumberOfTradesPerIteration = c.Int(nullable: false),
                        NumberOfIterations = c.Int(nullable: false),
                        TradingAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TradingAccounts", t => t.TradingAccountId, cascadeDelete: true)
                .Index(t => t.TradingAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonteCarloSimulations", "TradingAccountId", "dbo.TradingAccounts");
            DropIndex("dbo.MonteCarloSimulations", new[] { "TradingAccountId" });
            DropTable("dbo.MonteCarloSimulations");
        }
    }
}
