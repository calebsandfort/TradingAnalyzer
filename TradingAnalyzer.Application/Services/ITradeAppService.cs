using Abp.Application.Services;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities.Dtos;

namespace TradingAnalyzer.Services
{
    public interface ITradeAppService : IApplicationService
    {
        bool Save(TradeDto dto);
        List<TradeDto> GetAll();
        void Purge();
    }
}
