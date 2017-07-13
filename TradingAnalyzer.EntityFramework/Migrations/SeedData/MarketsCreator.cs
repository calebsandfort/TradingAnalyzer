using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities;
using TradingAnalyzer.EntityFramework;

namespace TradingAnalyzer.Migrations.SeedData
{
    public class MarketsCreator
    {
        private readonly TradingAnalyzerDbContext _context;

        public MarketsCreator(TradingAnalyzerDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.Markets.AddOrUpdate(
                    x => x.Symbol,
                    new Market { Name = "E-Mini NASDAQ 100", Symbol = "ES", TickSize = .25, TickValue = 5 },
                    new Market { Name = "E-Mini S&P 500", Symbol = "NQ", TickSize = .25, TickValue = 12.50 }
                );

            _context.SaveChanges();
        }
    }
}
