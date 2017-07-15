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
        readonly IObjectMapper _objectMapper;

        public TradingAccountAppService(IRepository<TradingAccount> tradingAccountRepository, ISqlExecuter sqlExecuter, IObjectMapper objectMapper)
        {
            this._tradingAccountRepository = tradingAccountRepository;
            this._sqlExecuter = sqlExecuter;
            _objectMapper = objectMapper;
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

            if (dto.Active) this.SetActive(dto.Id);
        }

        public void SetActive(int id)
        {
            foreach(TradingAccount tradingAccount in this._tradingAccountRepository.GetAllList())
            {
                tradingAccount.Active = tradingAccount.Id == id;
            }
        }

        public List<TradingAccountDto> GetAll()
        {
            return _objectMapper.Map<List<TradingAccountDto>>(_tradingAccountRepository.GetAll().OrderBy(x => x.Name).ToList());
        }
    }
}
