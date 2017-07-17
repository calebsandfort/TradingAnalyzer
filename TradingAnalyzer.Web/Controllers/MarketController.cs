using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradingAnalyzer.Entities;
using TradingAnalyzer.Entities.Dtos;
using TradingAnalyzer.Services;
using TradingAnalyzer.Web.Framework;

namespace TradingAnalyzer.Web.Controllers
{
    public class MarketController : TradingAnalyzerControllerBase
    {
        readonly IRepository<Market> _marketRepository;
        readonly IMarketAppService _marketAppService;
        readonly IObjectMapper _objectMapper;

        public MarketController(IRepository<Market> marketRepository, IMarketAppService marketAppService, IObjectMapper objectMapper)
        {
            _marketRepository = marketRepository;
            _marketAppService = marketAppService;
            _objectMapper = objectMapper;
        }

        // GET: Market
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMarkets()
        {
            return new GuerillaLogisticsApiJsonResult(_marketAppService.GetAll());
        }
    }
}