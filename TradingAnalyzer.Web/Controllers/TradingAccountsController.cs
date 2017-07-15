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

        [ChildActionOnly]
        public PartialViewResult TradingAccountMenu()
        {
            return PartialView("_TradingAccountMenu", this._tradingAccountAppService.GetAll());
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

        #region TradingAccount_Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TradingAccount_Create([DataSourceRequest] DataSourceRequest request, TradingAccountDto model)
        {
            if (model != null && ModelState.IsValid)
            {
                this._tradingAccountAppService.Save(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region TradingAccount_Update
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TradingAccount_Update([DataSourceRequest] DataSourceRequest request, TradingAccountDto model)
        {
            if (model != null && ModelState.IsValid)
            {
                this._tradingAccountAppService.Save(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region TradingAccount_Destroy
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TradingAccount_Destroy([DataSourceRequest] DataSourceRequest request, TradingAccountDto model)
        {
            if (model != null)
            {
                this._tradingAccountRepository.Delete(model.Id);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}