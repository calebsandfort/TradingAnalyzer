﻿using Abp.Application.Navigation;
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
                        "Dashboard",
                        new LocalizableString("Dashboard", TradingAnalyzerConsts.LocalizationSourceName),
                        url: "Dashboard",
                        icon: "fa fa-dashboard"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "MarketLog",
                        new LocalizableString("MarketLog", TradingAnalyzerConsts.LocalizationSourceName),
                        url: "MarketLog",
                        icon: "fa fa-pencil-square-o"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "TradingAccounts",
                        new LocalizableString("TradingAccounts", TradingAnalyzerConsts.LocalizationSourceName),
                        url: "TradingAccounts",
                        icon: "fa fa-book"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Markets",
                        new LocalizableString("Markets", TradingAnalyzerConsts.LocalizationSourceName),
                        url: "Markets",
                        icon: "fa fa-money"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "MonteCarloSimulations",
                        new LocalizableString("MonteCarloSimulations", TradingAnalyzerConsts.LocalizationSourceName),
                        url: "MonteCarloSimulations",
                        icon: "fa fa-cloud"
                        )
                );
        }
    }
}
