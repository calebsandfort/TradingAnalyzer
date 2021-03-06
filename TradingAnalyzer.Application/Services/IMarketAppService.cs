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
    public interface IMarketAppService : IApplicationService
    {
        List<MarketDto> GetAll();
        List<MarketDto> GetAllActive();
        MarketDto Get(int id);
        void Save(MarketDto dto);
    }
}
