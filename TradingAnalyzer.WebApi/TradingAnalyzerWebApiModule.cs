using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace TradingAnalyzer
{
    [DependsOn(typeof(AbpWebApiModule), typeof(TradingAnalyzerApplicationModule))]
    public class TradingAnalyzerWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(TradingAnalyzerApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
