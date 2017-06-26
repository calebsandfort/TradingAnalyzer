using Abp.Web.Mvc.Controllers;

namespace TradingAnalyzer.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class TradingAnalyzerControllerBase : AbpController
    {
        protected TradingAnalyzerControllerBase()
        {
            LocalizationSourceName = TradingAnalyzerConsts.LocalizationSourceName;
        }
    }
}