using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace TradingAnalyzer.Entities.Dtos
{
    [AutoMap(typeof(MarketLogEntry))]
    public class MarketLogEntryDto
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
        public String Text { get; set; }
        public virtual int TradingDayId { get; set; }
    }
}
