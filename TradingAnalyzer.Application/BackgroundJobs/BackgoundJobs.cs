using Abp.BackgroundJobs;
using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Services;

namespace TradingAnalyzer.BackgroundJobs
{
    public class RunMonteCarloSimulationBackgroundJob : BackgroundJob<MonteCarloSimulationDto>, ITransientDependency
    {
        private readonly IMonteCarloSimulationAppService _monteCarloSimulationAppService;

        public RunMonteCarloSimulationBackgroundJob(IMonteCarloSimulationAppService monteCarloSimulationAppService)
        {
            _monteCarloSimulationAppService = monteCarloSimulationAppService;
        }

        public override void Execute(MonteCarloSimulationDto args)
        {
            this._monteCarloSimulationAppService.RunSimulation(args);
        }
    }
}
