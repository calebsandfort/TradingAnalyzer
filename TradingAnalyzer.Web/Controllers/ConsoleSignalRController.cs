using System.Web.Mvc;
using TradingAnalyzer.Web.Hubs;
using TradingAnalyzer.Shared.Dtos;

namespace TradingAnalyzer.Web.Controllers
{
    //[AbpMvcAuthorize]
    public class ConsoleSignalRController : TradingAnalyzerControllerBase
    {
        private readonly ConsoleHub _consoleHub;

        public ConsoleSignalRController(ConsoleHub consoleHub)
        {
            _consoleHub = consoleHub;
        }

        [HttpPost]
        public ActionResult WriteLine(ConsoleWriteLineInput input)
        {
            this._consoleHub.WriteLine(input);
            return this.Json(new { success = true });
        }
    }
}