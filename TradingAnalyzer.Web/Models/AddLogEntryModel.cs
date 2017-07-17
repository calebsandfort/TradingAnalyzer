using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradingAnalyzer.Web.Models
{
    public class AddLogEntryModel
    {
        public DateTime Date { get; set; }
        public int MarketId { get; set; }
    }
}