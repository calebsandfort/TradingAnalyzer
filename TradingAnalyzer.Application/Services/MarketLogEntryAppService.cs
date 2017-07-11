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

namespace TradingAnalyzer.Services
{
    public class MarketLogEntryAppService : IMarketLogEntryAppService
    {
        readonly MarketLogEntryDomainService _marketLogEntryDomainService;

        public MarketLogEntryAppService(MarketLogEntryDomainService marketLogEntryDomainService)
        {
            this._marketLogEntryDomainService = marketLogEntryDomainService;
        }

        [UnitOfWork(IsDisabled = true)]
        public void Add(MarketLogEntryDto dto)
        {
            this._marketLogEntryDomainService.Add(dto);
        }
    }
}
