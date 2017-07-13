using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Entities
{
    public enum TradeTypes
    {
        None,
        [Display("Long")]
        Long,
        [Display("Short")]
        Short
    }
}
