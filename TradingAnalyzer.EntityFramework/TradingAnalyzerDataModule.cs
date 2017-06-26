using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using TradingAnalyzer.EntityFramework;

namespace TradingAnalyzer
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(TradingAnalyzerCoreModule))]
    public class TradingAnalyzerDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<TradingAnalyzerDbContext>(null);
        }
    }
}
