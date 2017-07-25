using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
    public class MonteCarloSimulationsController : TradingAnalyzerControllerBase
    {
        readonly IRepository<MonteCarloSimulation> _monteCarloSimulationRepository;
        readonly IMonteCarloSimulationAppService _monteCarloSimulationAppService;
        readonly ITradingAccountAppService _tradingAccountAppService;
        readonly IObjectMapper _objectMapper;

        public MonteCarloSimulationsController(IRepository<MonteCarloSimulation> monteCarloSimulationRepository,
            IMonteCarloSimulationAppService monteCarloSimulationAppService, ITradingAccountAppService tradingAccountAppService, IObjectMapper objectMapper)
        {
            _monteCarloSimulationRepository = monteCarloSimulationRepository;
            _monteCarloSimulationAppService = monteCarloSimulationAppService;
            _tradingAccountAppService = tradingAccountAppService;
            _objectMapper = objectMapper;
        }

        // GET: MonteCarloSimulations
        public ActionResult Index()
        {
            return View();
        }

        #region MonteCarloSimulations_Read
        public ActionResult MonteCarloSimulations_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();

            result.Data = _objectMapper.Map<List<MonteCarloSimulationDto>>(_monteCarloSimulationRepository.GetAll().Where(request.Filters).OrderBy(request.Sorts[0]).ToList());
            result.Total = _monteCarloSimulationRepository.GetAll().Where(request.Filters).Count();

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region MonteCarloSimulation_Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MonteCarloSimulation_Create([DataSourceRequest] DataSourceRequest request, MonteCarloSimulationDto model)
        {
            if (model != null && ModelState.IsValid)
            {
                this._monteCarloSimulationAppService.Save(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region MonteCarloSimulation_Update
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MonteCarloSimulation_Update([DataSourceRequest] DataSourceRequest request, MonteCarloSimulationDto model)
        {
            if (model != null && ModelState.IsValid)
            {
                this._monteCarloSimulationAppService.Save(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region MonteCarloSimulation_Destroy
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MonteCarloSimulation_Destroy([DataSourceRequest] DataSourceRequest request, MonteCarloSimulationDto model)
        {
            if (model != null)
            {
                this._monteCarloSimulationRepository.Delete(model.Id);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        public ActionResult MonteCarloSimulationModal(int id)
        {
            MonteCarloSimulationDto model = new MonteCarloSimulationDto();

            if (id == 0)
            {
                model.TimeStamp = DateTime.Now;
                model.TradingAccountId = this._tradingAccountAppService.GetActive().Id;
                model.NumberOfTradesPerIteration = 30;
                model.NumberOfIterations = 100;
            }
            else
            {
                MonteCarloSimulation monteCarloSimulation = this._monteCarloSimulationRepository.Single(x => x.Id == id);
                monteCarloSimulation.MapTo(model);
            }

            return PartialView("Modals/_MonteCarloSimulationModal", model);
        }
    }
}