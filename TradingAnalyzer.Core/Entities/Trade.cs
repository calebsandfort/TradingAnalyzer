using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities.Interfaces;

namespace TradingAnalyzer.Entities
{
    [Table("Trades")]
    public class Trade : EntityBase, IReconciliable
    {
        public int RefNumber { get; set; }

        [ForeignKey("MarketId")]
        public virtual Market Market { get; set; }
        public virtual int MarketId { get; set; }

        public int Timeframe { get; set; }
        public TradeTypes TradeType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EntryDate { get; set; }

        [DataType(DataType.Currency)]
        public Decimal EntryPrice { get; set; }
        public TradingSetups EntrySetups { get; set; }
        public String EntryRemarks { get; set; }
        public String EntryScreenshot { get; set; }

        //[DataType(DataType.Currency)]
        //public Decimal MFE { get; set; }

        //[DataType(DataType.Currency)]
        //public Decimal MFA { get; set; }

        [DataType(DataType.Currency)]
        public Decimal StopLossPrice { get; set; }
        [DataType(DataType.Currency)]
        public Decimal ProfitTakerPrice { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExitDate { get; set; }

        [DataType(DataType.Currency)]
        public Decimal ExitPrice { get; set; }
        public TradeExitReasons ExitReason { get; set; }
        public String ExitRemarks { get; set; }
        public String ExitScreenshot { get; set; }

        [DataType(DataType.Currency)]
        public Decimal Commissions { get; set; }

        public TradeClassifications Classification { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit/Loss")]
        public Decimal ProfitLoss { get; set; }

        public int Size { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Profit/Loss Per Contract")]
        public Decimal ProfitLossPerContract { get; set; }

        [ForeignKey("TradingAccountId")]
        public virtual TradingAccount TradingAccount { get; set; }
        public virtual int TradingAccountId { get; set; }

        [ForeignKey("TradingDayId")]
        public virtual TradingDay TradingDay { get; set; }
        public virtual int TradingDayId { get; set; }

        public void Reconcile()
        {
            this.Commissions = this.Size * 6.15m;
            this.ProfitLoss = (((this.TradeType == TradeTypes.Long ? this.ExitPrice - this.EntryPrice : this.EntryPrice - this.ExitPrice)/this.Market.TickSize) * this.Market.TickValue) - this.Commissions;
            this.ProfitLossPerContract = this.ProfitLoss / this.Size;
        }
    }
}
