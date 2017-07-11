namespace TradingAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogStuff : DbMigration
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
                        TradingDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TradingDays", t => t.TradingDayId, cascadeDelete: true)
                .Index(t => t.TradingDayId);
            
            CreateTable(
                "dbo.TradingDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MarketLogEntries", "TradingDayId", "dbo.TradingDays");
            DropIndex("dbo.MarketLogEntries", new[] { "TradingDayId" });
            DropTable("dbo.TradingDays");
            DropTable("dbo.MarketLogEntries");
        }
    }
}
