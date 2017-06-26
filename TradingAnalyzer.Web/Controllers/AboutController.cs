using System.Web.Mvc;

namespace TradingAnalyzer.Web.Controllers
{
    public class AboutController : TradingAnalyzerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}