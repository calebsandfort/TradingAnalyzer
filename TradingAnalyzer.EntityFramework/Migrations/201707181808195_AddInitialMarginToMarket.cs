namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitialMarginToMarket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "InitialMargin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Markets", "TickValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Markets", "TickSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Markets", "TickSize", c => c.Double(nullable: false));
            AlterColumn("dbo.Markets", "TickValue", c => c.Double(nullable: false));
            DropColumn("dbo.Markets", "InitialMargin");
        }
    }
}
