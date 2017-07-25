using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using TradingAnalyzer.Framework;
using Abp.Application.Services.Dto;

namespace TradingAnalyzer.Entities.Dtos
{
    public class MonteCarloSimulationDto : EntityDtoBase
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Sample Size")]
        public int NumberOfTradesInSample { get; set; }

        [UIHint("MyInt")]
        [Display(Name = "Trades/Iteration")]
        public int NumberOfTradesPerIteration { get; set; }

        [UIHint("MyInt")]
        [Display(Name = "Iterations")]
        public int NumberOfIterations { get; set; }

        [Display(Name = "Account")]
        public String TradingAccount { get; set; }
        [UIHint("TradingAccount")]
        [Display(Name = "Account")]
        public virtual int TradingAccountId { get; set; }
    }
}
