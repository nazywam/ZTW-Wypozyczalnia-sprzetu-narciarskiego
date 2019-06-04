using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SkiRent.Authorization;
using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using X.PagedList;
using SkiRent.Entities;
using SkiRent.Extensions;
using SkiRent.ViewModels.Order;
using SkiRent.ViewModels.Item;

namespace SkiRent.Controllers
{
	[Authorize]
    public class OrderController : BaseController
    {
	    private OrderService _orderService;
        private CustomerService _customerService;
        private ItemService _itemService;

        public OrderController()
	    {
			var model = new Model();
			_orderService = new OrderService(model);
            _customerService = new CustomerService(model);
            _itemService = new ItemService(model);
        }

	    [MvcSiteMapNode(Title = "[[[Order management]]]", ParentKey = "Home_Index", Key = "Order_Index")]
		public ActionResult Index(int? page)
        {
	        OrderIndexViewModel model = new OrderIndexViewModel();
	        model.OrderList = _orderService.GetAll().OrderByDescending(i=>i.Date_Rented).ToPagedList(page ?? 1, PAGE_SIZE);

			return View(GetIndexViewModel(model));
        }

        [HttpPost]
		public ActionResult Index(OrderFilterModel filterModel)
        {
	        OrderIndexViewModel model = new OrderIndexViewModel()
	        {
		        OrderList = filterModel.IsFiltered ? _orderService.Filter(filterModel).ToPagedList() : _orderService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel,
			};

			return View(GetIndexViewModel(model));
        }

		[MvcSiteMapNode(Title = "[[[Details]]]", ParentKey = "Order_Index", Key = "Order_Details", PreservedRouteParameters = "id")]
		public ActionResult Details(int id)
        {
            return View(_orderService.Get(id));
        }

		[MvcSiteMapNode(Title = "[[[Create]]]", ParentKey = "Order_Index", Key = "Order_Create")]
		public ActionResult Create()
		{
			OrderCreateViewModel vm = new OrderCreateViewModel()
			{
				Model = new OrderDetailViewModel()
			};
            return View(GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Create(OrderCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
            {
                viewModel.Model.EmployeeID = (User.Identity as User).Employee.ID;
                ServiceResult result = _orderService.Add(viewModel);
				if (result.Status)
		        {
			        AddSuccessCreatedToastMessage();
			        return RedirectToAction("Index");
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
			        return View(GetCreateViewModel(viewModel));
		        }
	        }
	        return View(GetCreateViewModel(viewModel));
		}

		[MvcSiteMapNode(Title = "[[[Edit]]]", ParentKey = "Order_Index", Key = "Order_Edit", PreservedRouteParameters = "id")]
		public ActionResult Edit(int id)
		{
			OrderCreateViewModel vm  = new OrderCreateViewModel();
			vm.Model = _orderService.Get(id);

			return View("Create", GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Edit(OrderCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _orderService.Update(viewModel);
		        if (result.Status)
		        {
			        AddSuccessEditedToastMessage();
			        return RedirectToAction("Index");
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
					return View("Create", GetCreateViewModel(viewModel));
				}
	        }
			return View("Create", GetCreateViewModel(viewModel));
		}

        //		[MvcSiteMapNode(Title = "[[[Delete]]]", ParentKey = "Order_Index", Key = "Order_Delete", PreservedRouteParameters = "id")]
        //		public ActionResult Delete(int id)
        //        {
        //            return View(_orderService.Get(id));
        //        }
        //
        //        [HttpPost]
        //		public ActionResult Delete(OrderDetailViewModel model)
        //        {
        //	        ServiceResult result = _orderService.Delete(model);
        //	        if (result.Status)
        //	        {
        //		        AddSuccessDeletedToastMessage();
        //		        return RedirectToAction("Index");
        //	        }
        //	        else
        //	        {
        //		        AddServiceErrorToastMessage(result);
        //		        return RedirectToAction("Delete", new { model.ID });
        //	        }
        //		}

        [MvcSiteMapNode(Title = "[[[Return]]]", ParentKey = "Order_Index", Key = "Order_Return", PreservedRouteParameters = "id")]
        public ActionResult Return(int id)
        {
            return View(_orderService.Get(id));
        }

        [HttpPost]
        public ActionResult Return(OrderDetailViewModel model)
        {
            ServiceResult result = _orderService.Return(model);
            if (result.Status)
            {
                AddToastMessage("[[[Success!]]]", "[[[Returned successfully.]]]", ToastType.Success);
                return RedirectToAction("Index");
            }
            else
            {
                AddServiceErrorToastMessage(result);
                return RedirectToAction("Return", new { model.ID });
            }
        }

        private OrderIndexViewModel GetIndexViewModel(OrderIndexViewModel currModel)
		{
			currModel.FilterModel = currModel.FilterModel ?? new OrderFilterModel();
            //To wlasciwie ta sama lista Tak = 1, Nie = 0
            currModel.ReturnedSelectList = _itemService.GetAvaibleSelectList();
            currModel.CustomerSelectList = _customerService.GetSelectCustomerList();
			return currModel;
		}

		private OrderCreateViewModel GetCreateViewModel(OrderCreateViewModel currModel)
        {
            currModel.CustomerSelectList = _customerService.GetSelectCustomerList();
            if (currModel.Model.Rented_Items != null)
            {
                currModel.ItemsFormList = new List<ItemBasicViewModel>();
                currModel.Model.Rented_Items.ForEach(item => currModel.ItemsFormList.Add(item.Item));
            }

            currModel.IsEditMode = currModel.Model.ID == 0 ? false : true;
            return currModel;
		}

        public ActionResult GetItemRowPartial(string barcode)
        {
            var item = _itemService.GetAvaibleItemByBarcode(barcode);
            if (item != null)
            {
                return PartialView("_ItemRow", item);
            }
            else
            {
                return null;
            }
        }

        public ActionResult ItemsAutocomplete(string term)
        {
            return Json(_itemService.GetAvaibleItemBarcodesByBarcode(term), JsonRequestBehavior.AllowGet);
        }

    }
}
