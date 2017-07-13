using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Entities
{
    [Flags]
    public enum TradingSetups
    {
        None = 0,
        [Display("CongestionZone")]
        CongestionZone = 1,
        [Display("Congestion Breakout Failure")]
        CongestionBreakoutFailure = 2,
        [Display("Trend Bar Failure")]
        TrendBarFailure = 4,
        [Display("Anti-Climax")]
        AntiClimax = 8,
        [Display("Deceleration")]
        Deceleration = 16,
        [Display("Anxiety Zone")]
        AnxietyZone = 32,
        [Display("Surge")]
        Surge = 64
    }
}
