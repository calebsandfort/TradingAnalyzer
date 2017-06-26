using System.Reflection;
using Abp.Modules;

namespace TradingAnalyzer
{
    public class TradingAnalyzerCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
