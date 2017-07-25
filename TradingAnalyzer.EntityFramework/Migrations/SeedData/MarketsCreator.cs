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
                    new Market { Name = "E-Mini NASDAQ 100", Symbol = "ES", TickSize = .25m, TickValue = 5, InitialMargin = 4620 },
                    new Market { Name = "E-Mini S&P 500", Symbol = "NQ", TickSize = .25m, TickValue = 12.50m, InitialMargin = 4290 },
                    new Market { Name = "E-Mini Dow", Symbol = "YM", TickSize = 1, TickValue = 5, InitialMargin = 3905 },
                    new Market { Name = "Gold", Symbol = "GC", TickSize = .10m, TickValue = 10, InitialMargin = 4345 },
                    new Market { Name = "Oil", Symbol = "CL", TickSize = .01m, TickValue = 10, InitialMargin = 2750 }
                );

            _context.SaveChanges();
        }
    }
}
