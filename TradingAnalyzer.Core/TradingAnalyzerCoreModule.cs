using System.Reflection;
using Abp.Modules;
using Abp.AutoMapper;

namespace TradingAnalyzer
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class TradingAnalyzerCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
