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
    public class TradingAccountAppService : ITradingAccountAppService
    {
        public readonly IRepository<TradingAccount> _tradingAccountRepository;
        public readonly ISqlExecuter _sqlExecuter;

        public TradingAccountAppService(IRepository<TradingAccount> tradingAccountRepository, ISqlExecuter sqlExecuter)
        {
            this._tradingAccountRepository = tradingAccountRepository;
            this._sqlExecuter = sqlExecuter;
        }

        public void Save(TradingAccountDto dto)
        {
            if (dto.IsNew)
            {
                TradingAccount tradingAccount = dto.MapTo<TradingAccount>();
                this._tradingAccountRepository.Insert(tradingAccount);
            }
            else
            {
                TradingAccount tradingAccount = this._tradingAccountRepository.Get(dto.Id);
                dto.MapTo(tradingAccount);
            }
        }
    }
}
