using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSiteMapProvider;
using SkiRent.Authorization;
using SkiRent.Entities;
using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.Services;
using SkiRent.ViewModels;
using SkiRent.ViewModels.Category;


namespace SkiRent.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatsController : BaseController
    {
        private StatsService _statsService;
        private static string DATE_PATTERN = "dd-MM-yyyy";

        public StatsController()
        {
            var model = new Model();
            _statsService = new StatsService(model);
        }

        [MvcSiteMapNode(Title = "[[[Stats]]]", ParentKey = "Home_Index", Key = "Stats_Index")]
        public ActionResult Index()
        {
            return View(GetIndexViewModel(new StatsViewModel()));
        }

        [HttpPost]
        public ActionResult Index(StatsFilterModel filterModel)
        {
            StatsViewModel vm = new StatsViewModel();
            DateTime dateTo;
            DateTime dateFrom;
            if (DateTime.TryParseExact(filterModel.S_DateFrom, DATE_PATTERN, null, DateTimeStyles.None, out dateFrom) &&
                DateTime.TryParseExact(filterModel.S_DateTo, DATE_PATTERN, null, DateTimeStyles.None, out dateTo))
            {
                if (dateTo >= dateFrom)
                {
                    vm = _statsService.GetStats(dateFrom, dateTo);
                    vm.FilterModel = filterModel;
                }
                else
                    AddToastMessage("Błąd", "Data do musi po dacie do", ToastType.Error);
            }
            else
            {
                AddToastMessage("Błąd", "Niewłaściwy format daty", ToastType.Error);
            }

            return View(GetIndexViewModel(vm));
        }

        private StatsViewModel GetIndexViewModel(StatsViewModel currModel)
        {
            if (currModel.FilterModel == null)
            {
                DateTime date = DateTime.Today;
                DateTime DefDateFrom = new DateTime(date.Year, date.Month, 1);
                DateTime DefDateTo = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                currModel = _statsService.GetStats(DefDateFrom, DefDateTo);
                currModel.FilterModel = new StatsFilterModel()
                {
                    S_DateFrom = DefDateFrom.ToString(DATE_PATTERN),
                    S_DateTo = DefDateTo.ToString(DATE_PATTERN)
                };
            }

            return currModel;
        }
    }
}