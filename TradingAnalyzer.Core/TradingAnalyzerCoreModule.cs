using System.Reflection;
using Abp.Modules;
using Abp.AutoMapper;
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Framework;
using TradingAnalyzer.Shared;

namespace TradingAnalyzer
{
    [DependsOn(typeof(AbpAutoMapperModule), typeof(TradingAnalyzerSharedModule))]
    public class TradingAnalyzerCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                #region Trade
                config.CreateMap<Trade, TradeDto>()
                              .ForMember(u => u.Market, options => options.MapFrom(input => input.Market.Symbol))
                              .ForMember(u => u.TradingAccount, options => options.MapFrom(input => input.TradingAccount.Name))
                              .ForMember(u => u.EntrySetups, options => options.MapFrom(input => EnumExtensions.FlaggedEnumToList<TradingSetups>(input.EntrySetups)));

                config.CreateMap<TradeDto, Trade>()
                      .ForMember(u => u.EntrySetups, options => options.MapFrom(input => EnumExtensions.ListToFlaggedEnum<TradingSetups>(input.EntrySetups)));
                #endregion

                #region MarketLogEntry
                config.CreateMap<MarketLogEntry, MarketLogEntryDto>()
                              .ForMember(u => u.Market, options => options.MapFrom(input => input.Market.Symbol));

                config.CreateMap<MarketLogEntryDto, MarketLogEntry>();
                #endregion

                #region MonteCarloSimulation
                config.CreateMap<MonteCarloSimulation, MonteCarloSimulationDto>()
                              .ForMember(u => u.TradingAccount, options => options.MapFrom(input => input.TradingAccount.Name));

                config.CreateMap<MonteCarloSimulationDto, MonteCarloSimulation>();
                #endregion

                //Configuration.Settings.Providers.Add<MySettingProvider>();
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
