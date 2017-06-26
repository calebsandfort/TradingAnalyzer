using System.Reflection;
using Abp.Modules;

namespace TradingAnalyzer
{
    [DependsOn(typeof(TradingAnalyzerCoreModule))]
    public class TradingAnalyzerApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
