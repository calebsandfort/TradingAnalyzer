using Abp.Domain.Repositories;
using Abp.ObjectMapping;
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
    public class TradingAccountsController : TradingAnalyzerControllerBase
    {
        readonly IRepository<TradingAccount> _tradingAccountRepository;
        readonly ITradingAccountAppService _tradingAccountAppService;
        readonly IObjectMapper _objectMapper;

        public TradingAccountsController(IRepository<TradingAccount> tradingAccountRepository, ITradingAccountAppService tradingAccountAppService, IObjectMapper objectMapper)
        {
            _tradingAccountRepository = tradingAccountRepository;
            _tradingAccountAppService = tradingAccountAppService;
            _objectMapper = objectMapper;
        }

        // GET: TradingAccounts
        public ActionResult Index()
        {
            return View();
        }

        #region TradingAccounts_Read
        public ActionResult TradingAccounts_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();

            result.Data = _objectMapper.Map<List<TradingAccountDto>>(_tradingAccountRepository.GetAll().Where(request.Filters).OrderBy(request.Sorts[0]).ToList());
            result.Total = _tradingAccountRepository.GetAll().Where(request.Filters).Count();

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region CalcNflPlayers_Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CalcNflPlayers_Create([DataSourceRequest] DataSourceRequest request, CalcNflPlayerJson model)
        {
            using (DfsAssistantContext context = new DfsAssistantContext())
            {
                if (model != null && ModelState.IsValid)
                {
                    CalcNflPlayer entity = model.ToEntity();
                    context.CalcNflPlayers.Add(entity);
                    context.SaveChanges();
                    model.Identifier = entity.Identifier;
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }
        #endregion

        #region CalcNflPlayers_Update
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CalcNflPlayers_Update([DataSourceRequest] DataSourceRequest request, CalcNflPlayerJson model)
        {
            using (DfsAssistantContext context = new DfsAssistantContext())
            {
                if (model != null && ModelState.IsValid)
                {
                    CalcNflPlayer entity = context.CalcNflPlayers.Single(x => x.Identifier == model.Identifier);
                    entity.Update(model);
                    context.SaveChanges();
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }
        #endregion

        #region CalcNflPlayers_Destroy
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CalcNflPlayers_Destroy([DataSourceRequest] DataSourceRequest request, CalcNflPlayerJson model)
        {
            using (DfsAssistantContext context = new DfsAssistantContext())
            {
                if (model != null)
                {
                    context.CalcNflPlayers.Remove(context.CalcNflPlayers.Single(x => x.Identifier == model.Identifier));
                    context.SaveChanges();
                }

                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }
        #endregion
    }
}