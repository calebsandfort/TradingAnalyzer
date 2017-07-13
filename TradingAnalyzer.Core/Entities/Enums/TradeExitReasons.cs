using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Entities
{
    public enum TradeExitReasons
    {
        None,
        [Display("Target Hit")]
        TargetHit,
        [Display("Stop Loss Hit")]
        StopLossHit,
        [Display("Reversal Signal")]
        ReversalSignal,
        [Display("End of Day")]
        EndOfDay
    }
}
