using Abp.Application.Navigation;
using Abp.Localization;

namespace TradingAnalyzer.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class TradingAnalyzerNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "MarketLog",
                        new LocalizableString("MarketLog", TradingAnalyzerConsts.LocalizationSourceName),
                        url: "MarketLog",
                        icon: "fa fa-pencil-square-o"
                        )
                );
        }
    }
}
