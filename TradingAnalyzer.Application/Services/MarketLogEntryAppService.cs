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
    public class MarketLogEntryAppService : IMarketLogEntryAppService
    {
        readonly IMarketLogEntryDomainService _marketLogEntryDomainService;
        public readonly IRepository<MarketLogEntry> _marketLogEntryRepository;
        public readonly ISqlExecuter _sqlExecuter;

        public MarketLogEntryAppService(IMarketLogEntryDomainService marketLogEntryDomainService, IRepository<MarketLogEntry> marketLogEntryRepository, ISqlExecuter sqlExecuter)
        {
            this._marketLogEntryDomainService = marketLogEntryDomainService;
            this._marketLogEntryRepository = marketLogEntryRepository;
            this._sqlExecuter = sqlExecuter;
        }

        public void Add(MarketLogEntryDto dto)
        {
            this._marketLogEntryDomainService.Add(dto);
        }

        public void Purge()
        {
            foreach(MarketLogEntry entry in this._marketLogEntryRepository.GetAll().Where(x => x.TradingAccount.Active))
            {
                this._marketLogEntryRepository.Delete(entry.Id);
            }
        }
    }
}
