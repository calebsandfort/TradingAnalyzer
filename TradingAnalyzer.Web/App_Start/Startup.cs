using Abp.Owin;
using Microsoft.Owin;
using Owin;
using TradingAnalyzer.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace TradingAnalyzer.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            app.MapSignalR();
        }
    }
}