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
    public class MarketAppService : IMarketAppService
    {
        public readonly IRepository<Market> _marketRepository;
        public readonly ISqlExecuter _sqlExecuter;
        readonly IObjectMapper _objectMapper;

        public MarketAppService(IRepository<Market> marketRepository, ISqlExecuter sqlExecuter, IObjectMapper objectMapper)
        {
            this._marketRepository = marketRepository;
            this._sqlExecuter = sqlExecuter;
            _objectMapper = objectMapper;
        }

        public MarketDto Get(int id)
        {
            return _marketRepository.Get(id).MapTo<MarketDto>();
        }

        public List<MarketDto> GetAll()
        {
            return _objectMapper.Map<List<MarketDto>>(_marketRepository.GetAll().OrderBy(x => x.Symbol).ToList());
        }

        public void Save(MarketDto dto)
        {
            if (dto.IsNew)
            {
                Market market = dto.MapTo<Market>();
                this._marketRepository.Insert(market);
            }
            else
            {
                Market market = this._marketRepository.Get(dto.Id);
                dto.MapTo(market);
            }
        }
    }
}
