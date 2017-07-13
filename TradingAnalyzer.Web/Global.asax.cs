using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using TradingAnalyzer.Entities;
using System.Web.Mvc;

namespace TradingAnalyzer.Web
{
    public class MvcApplication : AbpWebApplication<TradingAnalyzerWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            ModelBinders.Binders.Add(typeof(TradingDirectiveTypes), new EnumModelBinder<TradingDirectiveTypes>());
            ModelBinders.Binders.Add(typeof(MarketLogEntryTypes), new EnumModelBinder<MarketLogEntryTypes>());

            base.Application_Start(sender, e);
        }
    }
}
