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
    [Table("MonteCarloSimulations")]
    public class MonteCarloSimulation : EntityBase
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        public int NumberOfTradesInSample { get; set; }
        public int NumberOfTradesPerIteration { get; set; }
        public int NumberOfIterations { get; set; }

        [ForeignKey("TradingAccountId")]
        public virtual TradingAccount TradingAccount { get; set; }
        public virtual int TradingAccountId { get; set; }
    }
}
