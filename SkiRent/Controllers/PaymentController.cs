using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using X.PagedList;
using SkiRent.Entities;
using SkiRent.Extensions;
using SkiRent.ViewModels.Payment;
using SkiRent.ViewModels.Item;

namespace SkiRent.Controllers
{
	[Authorize]
    public class PaymentController : BaseController
    {
	    private PaymentService _paymentService;

	    public PaymentController()
	    {
			var model = new Model();
			_paymentService = new PaymentService(model);
	    }

        [MvcSiteMapNode(Title = "[[[Index]]]", Key = "Payment_Index", ParentKey = "Home_Index")]
		public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "Home");
        }

		[MvcSiteMapNode(Title = "[[[Create]]]", ParentKey = "Payment_Index", Key = "Payment_Create")]
		public ActionResult Create(int id)
		{
			PaymentCreateViewModel vm = new PaymentCreateViewModel()
			{
				Model = new PaymentBasicViewModel()
                {
                    OrderID = id,
                    PaymentDate = DateTime.Now,
                    Currency = "PLN",
                    ExchangeRate = 1
                }
			};
            return View(GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Create(PaymentCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _paymentService.Add(viewModel.Model);
				if (result.Status)
		        {
			        AddSuccessCreatedToastMessage();
			        return RedirectToAction("Details", "Order", new { id = viewModel.Model.OrderID});
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
			        return View(GetCreateViewModel(viewModel));
		        }
	        }
	        return View(GetCreateViewModel(viewModel));
		}

		[MvcSiteMapNode(Title = "[[[Edit]]]", ParentKey = "Payment_Index", Key = "Payment_Edit", PreservedRouteParameters = "id")]
		public ActionResult Edit(int id)
		{
			PaymentCreateViewModel vm  = new PaymentCreateViewModel();
			vm.Model = _paymentService.Get(id);

			return View("Create", GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Edit(PaymentCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _paymentService.Update(viewModel.Model);
		        if (result.Status)
		        {
			        AddSuccessEditedToastMessage();
                    return RedirectToAction("Details", "Order", new { id = viewModel.Model.OrderID });
                }
		        else
		        {
			        AddServiceErrorToastMessage(result);
					return View("Create", GetCreateViewModel(viewModel));
				}
	        }
			return View("Create", GetCreateViewModel(viewModel));
		}

		[MvcSiteMapNode(Title = "[[[Delete]]]", ParentKey = "Payment_Index", Key = "Payment_Delete", PreservedRouteParameters = "id")]
		public ActionResult Delete(int id)
        {
            return View(_paymentService.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(PaymentBasicViewModel model)
        {
            ServiceResult result = _paymentService.Delete(model);
            if (result.Status)
            {
                AddSuccessDeletedToastMessage();
                return RedirectToAction("Details", "Order", new { id = model.OrderID });
            }
            else
            {
                AddServiceErrorToastMessage(result);
                return RedirectToAction("Delete", new {model.ID});
            }
        }

        private PaymentCreateViewModel GetCreateViewModel(PaymentCreateViewModel currModel)
		{
            return currModel;
		}

        public ActionResult GetExchangeTable()
        {
            Dictionary<string, string> dic = NBPApiService.GetExchangeTable();

            var ans = PartialView("_ExchangeTable", dic);

            return ans;
        }
    }
}
