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
    [Table("MarketLogEntries")]
    public class MarketLogEntry : Entity<int>
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public String Text { get; set; }
        public String Screenshot { get; set; }

        public MarketLogEntryTypes MarketLogEntryType { get; set; }

        [ForeignKey("TradingDayId")]
        public virtual TradingDay TradingDay { get; set; }
        public virtual int TradingDayId { get; set; }
    }
}
