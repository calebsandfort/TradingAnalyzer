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
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Shared.SqlExecuter;

namespace TradingAnalyzer.Services
{
    public class TradingDayAppService : ITradingDayAppService
    {
        public readonly IRepository<TradingDay> _tradingDayRepository;
        public readonly ISqlExecuter _sqlExecuter;
        readonly IObjectMapper _objectMapper;

        public TradingDayAppService(IRepository<TradingDay> tradingDayRepository, ISqlExecuter sqlExecuter, IObjectMapper objectMapper)
        {
            this._tradingDayRepository = tradingDayRepository;
            this._sqlExecuter = sqlExecuter;
            _objectMapper = objectMapper;
        }

        public TradingDayDto Get(DateTime date)
        {
            TradingDay tradingDay = _tradingDayRepository.FirstOrDefault(x => x.Day.Year == date.Year && x.Day.Month == date.Month && x.Day.Day == date.Day);
            if (tradingDay == null)
            {
                tradingDay = new TradingDay();
                tradingDay.Day = date.Date;
                this._tradingDayRepository.Insert(tradingDay);
            }

            return tradingDay.MapTo<TradingDayDto>();
        }


    }
}
