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
    public class TradingAccount : Entity<int>
    {
        public String Name { get; set; }

        [DataType(DataType.Currency)]
        public Double InitialCapital { get; set; }

        [DataType(DataType.Currency)]
        public Double CurrentCapital { get; set; }

        [DataType(DataType.Currency)]
        public Double MiscFees { get; set; }

        [DataType(DataType.Currency)]
        public Double CommissionsAndFees { get; set; }

        [DataType(DataType.Currency)]
        public Double GrossPandL { get; set; }

        [DataType(DataType.Currency)]
        public Double NetPandL { get; set; }

        [ForeignKey("TradingAccountId")]
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
