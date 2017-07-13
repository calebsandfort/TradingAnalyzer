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
    [Table("Markets")]
    public class Market : Entity<int>
    {
        public String Name { get; set; }
        public String Symbol { get; set; }

        [DataType(DataType.Currency)]
        public Double TickValue { get; set; }
        public Double TickSize { get; set; }

        [DataType(DataType.Currency)]
        [NotMapped]
        public Double PointValue
        {
            get
            {
                return 1.0 / this.TickSize * this.TickValue;
            }
        }

        [ForeignKey("MarketId")]
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
