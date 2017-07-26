using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Framework
{
    public static class Extensions
    {
        public static T Percentile<T>(this List<T> data, Decimal k)
        {
            data = data.OrderBy(x => x).ToList();
            int index = ((int)Math.Ceiling(k * data.Count)) - 1;

            return data.Take(index).Last();
        }
    }
}
