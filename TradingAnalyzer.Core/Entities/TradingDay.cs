using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Entities
{
    public class TradingDay : Entity<int>
    {
        [DataType(DataType.DateTime)]
        public DateTime Day { get; set; }

        [ForeignKey("TradingDayId")]
        public virtual ICollection<MarketLogEntry> MarketLogEntries { get; set; }
    }
}
