using Abp.Web.Mvc.Views;

namespace TradingAnalyzer.Web.Views
{
    public abstract class TradingAnalyzerWebViewPageBase : TradingAnalyzerWebViewPageBase<dynamic>
    {

    }

    public abstract class TradingAnalyzerWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected TradingAnalyzerWebViewPageBase()
        {
            LocalizationSourceName = TradingAnalyzerConsts.LocalizationSourceName;
        }
    }
}