using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradingAnalyzer.Web.Controllers
{
    public class ViewRendererController : TradingAnalyzerControllerBase
    {
        public ActionResult AddLogEntryModal()
        {
            return PartialView("Modals/_AddLogEntryModal");
        }
    }
}