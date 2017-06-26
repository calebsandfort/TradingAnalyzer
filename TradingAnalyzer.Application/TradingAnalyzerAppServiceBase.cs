using Abp.Application.Services;

namespace TradingAnalyzer
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class TradingAnalyzerAppServiceBase : ApplicationService
    {
        protected TradingAnalyzerAppServiceBase()
        {
            LocalizationSourceName = TradingAnalyzerConsts.LocalizationSourceName;
        }
    }
}