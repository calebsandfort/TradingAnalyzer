using Abp.Dependency;
using TradingAnalyzer.Shared.Dtos;
using Microsoft.AspNet.SignalR;

namespace TradingAnalyzer.Web.Hubs
{
    public class ConsoleHub : Hub, ISingletonDependency
    {
        public void WriteLine(ConsoleWriteLineInput input)
        {
            Clients.All.writeLine(input.Line);
        }
    }
}