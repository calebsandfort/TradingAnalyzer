using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Entities.Dtos
{
    public class TradeDto : EntityDtoBase
    {
        public int RefNumber { get; set; }

        public String Market { get; set; }
        public int MarketId { get; set; }

        public int Timeframe { get; set; }
        public TradeTypes TradeType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EntryDate { get; set; }

        [DataType(DataType.Currency)]
        public Double EntryPrice { get; set; }
        public TradingSetups EntrySetups { get; set; }
        public String EntryRemarks { get; set; }
        public String EntryScreenshot { get; set; }

        [DataType(DataType.Currency)]
        public Double MFE { get; set; }

        [DataType(DataType.Currency)]
        public Double MFA { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExitDate { get; set; }

        [DataType(DataType.Currency)]
        public Double ExitPrice { get; set; }
        public TradeExitReasons ExitReason { get; set; }
        public String ExitRemarks { get; set; }
        public String ExitScreenshot { get; set; }

        [DataType(DataType.Currency)]
        public Double Commissions { get; set; }

        public TradeClassifications Classification { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit/Loss")]
        public Double ProfitLoss { get; set; }

        public int Size { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit/Loss Per Contract")]
        public Double ProfitLossPerContract { get; set; }

        public String TradingAccount { get; set; }
        public int TradingAccountId { get; set; }
    }
}
