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
    [Table("Trades")]
    public class Trade : Entity<int>
    {
        [DataType(DataType.DateTime)]
        public DateTime Opened { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Closed { get; set; }

        public int RefNumber { get; set; }
        public TradeTypes TradeType { get; set; }
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public Double MiscFees { get; set; }

        [DataType(DataType.Currency)]
        public Double CommissionsAndFees { get; set; }

        [DataType(DataType.Currency)]
        public Double GrossPandL { get; set; }

        [DataType(DataType.Currency)]
        public Double NetPandL { get; set; }

        [ForeignKey("TradingAccountId")]
        public virtual TradingAccount TradingAccount { get; set; }
        public virtual int TradingAccountId { get; set; }
    }
}
