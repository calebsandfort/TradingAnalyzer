﻿using Abp.Application.Services;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities.Dtos;

namespace TradingAnalyzer.Services
{
    public interface IMonteCarloSimulationAppService : IApplicationService
    {
        void Save(MonteCarloSimulationDto dto);
        void RunSimulationEnqueue(MonteCarloSimulationDto dto);
        void RunSimulation(MonteCarloSimulationDto dto);
        List<MonteCarloSimulationDto> GetAll();
        void Purge();
    }
}
