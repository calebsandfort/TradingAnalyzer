using Abp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Framework
{
    public class MySettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                {
                    new SettingDefinition(
                        "Path",
                        "http://localhost/TradingAnalyzer.Web/",
                        scopes: SettingScopes.Application
                        )
                };
        }
    }
}
