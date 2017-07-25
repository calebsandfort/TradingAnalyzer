namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMttToMarket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "MTT", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markets", "MTT");
        }
    }
}
