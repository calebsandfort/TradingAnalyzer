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
    public interface IScreenshotAppService : IApplicationService
    {
        ScreenshotDto Get(int id);
        void Save(ScreenshotDto dto);
        ScreenshotDto SaveBase64(String base64);
        void ConvertAll();
    }
}
