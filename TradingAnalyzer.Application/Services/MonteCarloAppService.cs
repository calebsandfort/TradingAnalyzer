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
using Abp.BackgroundJobs;
using TradingAnalyzer.Shared;

namespace TradingAnalyzer.Services
{
    public class MonteCarloSimulationAppService : AppServiceBase, IMonteCarloSimulationAppService
    {
        public readonly IRepository<MonteCarloSimulation> _repository;
        public readonly IRepository<Trade> _tradeRepository;

        public MonteCarloSimulationAppService(ISqlExecuter sqlExecuter, IConsoleHubProxy consoleHubProxy, IBackgroundJobManager backgroundJobManager, IObjectMapper objectMapper,
            IRepository<MonteCarloSimulation> repository, IRepository<Trade> tradeRepository)
            : base(sqlExecuter, consoleHubProxy, backgroundJobManager, objectMapper)
        {
            this._repository = repository;
            this._tradeRepository = tradeRepository;
        }

        public void Save(MonteCarloSimulationDto dto)
        {
            if (dto.IsNew)
            {
                MonteCarloSimulation monteCarloSimulation = dto.MapTo<MonteCarloSimulation>();
                this._repository.Insert(monteCarloSimulation);
            }
            else
            {
                MonteCarloSimulation monteCarloSimulation = this._repository.Get(dto.Id);
                dto.MapTo(monteCarloSimulation);
            }
        }

        public List<MonteCarloSimulationDto> GetAll()
        {
            return _objectMapper.Map<List<MonteCarloSimulationDto>>(_repository.GetAll().OrderByDescending(x => x.TimeStamp).ToList());
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
