using System.Reflection;
using Abp.Modules;
using Abp.AutoMapper;
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Framework;
using TradingAnalyzer.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                              .ForMember(u => u.EntrySetups, options => options.MapFrom(input => EnumExtensions.FlaggedEnumToList<TradingSetups>(input.EntrySetups)))
                              .ForMember(u => u.EntryScreenshotDbId, options => options.MapFrom(input => input.EntryScreenshotDbId.HasValue ? input.EntryScreenshotDbId.Value : 0))
                              .ForMember(u => u.ExitScreenshotDbId, options => options.MapFrom(input => input.ExitScreenshotDbId.HasValue ? input.ExitScreenshotDbId.Value : 0));

                config.CreateMap<TradeDto, Trade>()
                      .ForMember(u => u.EntrySetups, options => options.MapFrom(input => EnumExtensions.ListToFlaggedEnum<TradingSetups>(input.EntrySetups)));
                #endregion

                #region MarketLogEntry
                config.CreateMap<MarketLogEntry, MarketLogEntryDto>()
                              .ForMember(u => u.Market, options => options.MapFrom(input => input.Market.Symbol))
                              .ForMember(u => u.ScreenshotDbId, options => options.MapFrom(input => input.ScreenshotDbId.HasValue ? input.ScreenshotDbId.Value : 0));

                config.CreateMap<MarketLogEntryDto, MarketLogEntry>();
                #endregion

                #region MonteCarloSimulation
                config.CreateMap<MonteCarloSimulation, MonteCarloSimulationDto>()
                              .ForMember(u => u.TradingAccount, options => options.MapFrom(input => input.TradingAccount.Name))
                              .ForMember(u => u.MarketMaxContractsList, options => options.MapFrom(input => JsonConvert.DeserializeObject<List<MarketMaxContracts>>(input.MarketMaxContractsJson)));

                config.CreateMap<MonteCarloSimulationDto, MonteCarloSimulation>()
                    .ForMember(u => u.TradingAccount, options => options.Ignore())
                    .ForMember(u => u.MarketMaxContractsJson, options => options.MapFrom(input => JsonConvert.SerializeObject(input.MarketMaxContractsList)));
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
