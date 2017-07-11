using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradingAnalyzer.Web.Controllers
{
    public class MarketLogController : TradingAnalyzerControllerBase
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }
    }
}