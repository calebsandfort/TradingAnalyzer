using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Entities
{
    public enum TradeClassifications
    {
        None,
        [EnumDisplay("Consistent")]
        Consistent,
        [EnumDisplay("Discretionary")]
        Discretionary,
        [EnumDisplay("Rogue")]
        Rogue
    }
}
