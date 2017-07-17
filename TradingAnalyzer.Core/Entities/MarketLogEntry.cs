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
    public class MarketLogEntry : EntityBase
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public String Text { get; set; }
        public String Screenshot { get; set; }

        public MarketLogEntryTypes MarketLogEntryType { get; set; }

        [ForeignKey("TradingDayId")]
        public virtual TradingDay TradingDay { get; set; }
        public virtual int TradingDayId { get; set; }

        [ForeignKey("TradingAccountId")]
        public virtual TradingAccount TradingAccount { get; set; }
        public virtual int TradingAccountId { get; set; }

        [ForeignKey("MarketId")]
        public virtual Market Market { get; set; }
        public virtual int MarketId { get; set; }
    }
}
