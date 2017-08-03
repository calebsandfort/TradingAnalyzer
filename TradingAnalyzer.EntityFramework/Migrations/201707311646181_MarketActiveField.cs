namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarketActiveField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markets", "Active");
        }
    }
}
