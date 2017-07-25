using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Services;
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Shared.SqlExecuter;
using TradingAnalyzer.Framework;
using Abp.Timing;

namespace TradingAnalyzer.Services
{
    public class TradeAppService : ITradeAppService
    {
        public readonly IRepository<Trade> _tradeRepository;
        public readonly IRepository<Market> _marketRepository;
        public readonly ITradingAccountAppService _tradingAccountAppService;
        public readonly ITradingDayAppService _tradingDayAppService;
        readonly IRepository<MarketLogEntry> _marketLogEntryRepository;
        public readonly ISqlExecuter _sqlExecuter;
        readonly IObjectMapper _objectMapper;

        public TradeAppService(IRepository<Trade> tradeRepository, IRepository<Market> marketRepository, ITradingAccountAppService tradingAccountAppService,
            ITradingDayAppService tradingDayAppService, IRepository<MarketLogEntry> marketLogEntryRepository, ISqlExecuter sqlExecuter, IObjectMapper objectMapper)
        {
            this._tradeRepository = tradeRepository;
            this._marketRepository = marketRepository;
            this._tradingAccountAppService = tradingAccountAppService;
            this._tradingDayAppService = tradingDayAppService;
            this._marketLogEntryRepository = marketLogEntryRepository;
            this._sqlExecuter = sqlExecuter;
            _objectMapper = objectMapper;
        }

        public bool Save(TradeDto dto)
        {
            bool reconcileTradingAccount = false;
            Trade trade = new Trade();

            if (dto.IsNew)
            {
                trade = dto.MapTo<Trade>();
                trade.TradingAccountId = this._tradingAccountAppService.GetActive().Id;
                trade.TradingDayId = this._tradingDayAppService.Get(trade.EntryDate).Id;

                MarketLogEntry tradeEnterLogEntry = new MarketLogEntry();
                tradeEnterLogEntry.MarketId = trade.MarketId;
                tradeEnterLogEntry.MarketLogEntryType = MarketLogEntryTypes.TradeEnter;
                if (!String.IsNullOrEmpty(trade.EntryScreenshot)) tradeEnterLogEntry.Screenshot = trade.EntryScreenshot;
                tradeEnterLogEntry.Text = String.Format("{0} {1} @ {2:C}<br/>{3}", trade.TradeType == TradeTypes.Long ? "Buy" : "Sell", trade.Size, trade.EntryPrice, trade.EntryRemarks);
                tradeEnterLogEntry.TimeStamp = trade.EntryDate;
                tradeEnterLogEntry.TradingAccountId = trade.TradingAccountId;
                tradeEnterLogEntry.TradingDayId = trade.TradingDayId;

                this._marketLogEntryRepository.Insert(tradeEnterLogEntry);

                if (trade.ExitReason != TradeExitReasons.None)
                {
                    trade.Market = this._marketRepository.Get(trade.MarketId);
                    trade.Reconcile();
                    reconcileTradingAccount = true;
                }
            }
            else
            {
                trade = this._tradeRepository.Get(dto.Id);
                bool exitReasonChanged = dto.ExitReason != trade.ExitReason && dto.ExitReason != TradeExitReasons.None;
                dto.MapTo(trade);

                if (exitReasonChanged)
                {
                    trade.Market = this._marketRepository.Get(trade.MarketId);
                    trade.Reconcile();
                    reconcileTradingAccount = true;
                }
            }

            if (reconcileTradingAccount)
            {
                MarketLogEntry tradeExitLogEntry = new MarketLogEntry();
                tradeExitLogEntry.MarketId = trade.MarketId;
                tradeExitLogEntry.MarketLogEntryType = MarketLogEntryTypes.TradeExit;
                if (!String.IsNullOrEmpty(trade.ExitScreenshot)) tradeExitLogEntry.Screenshot = trade.ExitScreenshot;
                tradeExitLogEntry.Text = String.Format("{0}: {1} {2} @ {3:C}, P/L: {4:C}<br/>{5}", trade.ExitReason.GetDisplay(), trade.TradeType == TradeTypes.Long ? "Sell" : "Buy", trade.Size, trade.ExitPrice, trade.ProfitLoss, trade.ExitRemarks);
                tradeExitLogEntry.TimeStamp = trade.ExitDate.Value;
                tradeExitLogEntry.TradingAccountId = trade.TradingAccountId;
                tradeExitLogEntry.TradingDayId = trade.TradingDayId;
                this._marketLogEntryRepository.Insert(tradeExitLogEntry);
            }

            if (dto.IsNew)
            {
                this._tradeRepository.Insert(trade);
            }

            return reconcileTradingAccount;
        }

        public List<TradeDto> GetAll()
        {
            return _objectMapper.Map<List<TradeDto>>(_tradeRepository.GetAll().OrderByDescending(x => x.EntryDate).ToList());
        }

        public void Purge()
        {
            foreach (Trade trade in this._tradeRepository.GetAll().Where(x => x.TradingAccount.Active))
            {
                this._tradeRepository.Delete(trade.Id);
            }
        }
    }
}
