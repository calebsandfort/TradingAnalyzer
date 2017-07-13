using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Web.Framework;

namespace TradingAnalyzer.Web.Controllers
{
    public class MarketLogController : TradingAnalyzerControllerBase
    {
        readonly IRepository<MarketLogEntry> _marketLogEntryRepository;
        readonly IRepository<TradingDirective> _tradingDirectiveRepository;
        readonly IObjectMapper _objectMapper;

        public MarketLogController(IRepository<MarketLogEntry> marketLogEntryRepository, IRepository<TradingDirective> tradingDirectiveRepository, IObjectMapper objectMapper)
        {
            _marketLogEntryRepository = marketLogEntryRepository;
            _tradingDirectiveRepository = tradingDirectiveRepository;
            _objectMapper = objectMapper;
        }

        // GET: Log
        public ActionResult Index()
        {
            return View();
        }

        #region MarketLog_Read
        public ActionResult MarketLog_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();

            //DateTime currentDate = Clock.Now;

            //Expression<Func<NbaGame, bool>> todayFunc = x => x.Date.Year == currentDate.Year && x.Date.Month == currentDate.Month && x.Date.Day == currentDate.Day;

            //result.Data = _objectMapper.Map<List<MarketLogEntryDto>>(_movieRepository.GetAllIncluding(x => x.StatLines.Select(y => y.Participant)).Where(request.Filters).Where(todayFunc).OrderBy(request.Sorts[0]).ToList());
            //result.Total = _marketLogEntryRepository.GetAll().Where(request.Filters).Where(todayFunc).Count();

            result.Data = _objectMapper.Map<List<MarketLogEntryDto>>(_marketLogEntryRepository.GetAll().OrderByDescending(x => x.TimeStamp).ToList());

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        public ActionResult AddLogEntryModal()
        {
            DateTime lastEntryDate = DateTime.Now;
            if(this._marketLogEntryRepository.Count() > 0)
            {
                lastEntryDate = this._marketLogEntryRepository.GetAll().OrderByDescending(x => x.TimeStamp).First().TimeStamp;
            }

            return PartialView("Modals/_AddLogEntryModal", lastEntryDate);
        }
    }
}