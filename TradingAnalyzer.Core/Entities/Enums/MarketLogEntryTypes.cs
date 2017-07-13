using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Entities
{
    public enum MarketLogEntryTypes
    {
        [Display("None")]
        None,
        [Display("Market Analysis")]
        MarketAnalysis,
        [Display("Profit Target")]
        ProfitTarget,
        [Display("Trade Enter")]
        TradeEnter,
        [Display("Trade Exit")]
        TradeExit
    }
}
