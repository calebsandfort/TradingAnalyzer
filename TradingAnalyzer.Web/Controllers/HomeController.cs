using System.Web.Mvc;

namespace TradingAnalyzer.Web.Controllers
{
    public class HomeController : TradingAnalyzerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}