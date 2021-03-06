﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Entities
{
    public enum MarketLogEntryTypes
    {
        [EnumDisplay("None")]
        None,
        [EnumDisplay("Market Analysis")]
        MarketAnalysis,
        [EnumDisplay("Profit Target")]
        ProfitTarget,
        [EnumDisplay("Trade Enter")]
        TradeEnter,
        [EnumDisplay("Trade Exit")]
        TradeExit
    }
}
