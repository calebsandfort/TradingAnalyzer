﻿using System.Reflection;
using Abp.Modules;
using Abp.AutoMapper;
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;

namespace TradingAnalyzer
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class TradingAnalyzerCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Trade, TradeDto>()
                      .ForMember(u => u.Market, options => options.MapFrom(input => input.Market.Symbol))
                      .ForMember(u => u.TradingAccount, options => options.MapFrom(input => input.TradingAccount.Name));

                config.CreateMap<TradeDto, Trade>();
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
