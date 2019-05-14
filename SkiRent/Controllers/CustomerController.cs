using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using SkiRent.ViewModels.Employee;
using X.PagedList;
using SkiRent.Extensions;
using SkiRent.ViewModels.Customer;
using SkiRent.ViewModels.Item;

namespace SkiRent.Controllers
{
	[Authorize]
    public class CustomerController : BaseController
    {
	    private CustomerService _customerService;

	    public CustomerController()
	    {
			var model = new Model();
			_customerService = new CustomerService(model);
	    }

	    [MvcSiteMapNode(Title = "[[[Customer management]]]", ParentKey = "Home_Index", Key = "Customer_Index")]
		public ActionResult Index(int? page)
        {
	        CustomerIndexViewModel model = new CustomerIndexViewModel();
	        model.CustomerList = _customerService.GetAll().ToPagedList(page ?? 1, PAGE_SIZE);

			return View(GetIndexViewModel(model));
        }

        [HttpPost]
		public ActionResult Index(CustomerFilterModel filterModel)
        {
	        CustomerIndexViewModel model = new CustomerIndexViewModel()
	        {
		        CustomerList = filterModel.IsFiltered ? _customerService.Filter(filterModel).ToPagedList() : _customerService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel,
			};

			return View(GetIndexViewModel(model));
        }

		[MvcSiteMapNode(Title = "[[[Details]]]", ParentKey = "Customer_Index", Key = "Customer_Details", PreservedRouteParameters = "id")]
		public ActionResult Details(int id)
        {
            return View(_customerService.Get(id));
        }

		[MvcSiteMapNode(Title = "[[[Create]]]", ParentKey = "Customer_Index", Key = "Customer_Create")]
		public ActionResult Create()
		{
			CustomerCreateViewModel vm = new CustomerCreateViewModel()
			{
				Model = new CustomerDetailViewModel()
			};
            return View(GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Create(CustomerCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _customerService.Add(viewModel.Model);
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

		[MvcSiteMapNode(Title = "[[[Edit]]]", ParentKey = "Customer_Index", Key = "Customer_Edit", PreservedRouteParameters = "id")]
		public ActionResult Edit(int id)
		{
			CustomerCreateViewModel vm  = new CustomerCreateViewModel();
			vm.Model = _customerService.Get(id);

			return View("Create", GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Edit(CustomerCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _customerService.Update(viewModel.Model);
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

		[MvcSiteMapNode(Title = "[[[Delete]]]", ParentKey = "Customer_Index", Key = "Customer_Delete", PreservedRouteParameters = "id")]
		public ActionResult Delete(int id)
        {
            return View(_customerService.Get(id));
        }

        [HttpPost]
		public ActionResult Delete(CustomerDetailViewModel model)
        {
	        ServiceResult result = _customerService.Delete(model);
	        if (result.Status)
	        {
		        AddSuccessDeletedToastMessage();
		        return RedirectToAction("Index");
	        }
	        else
	        {
		        AddServiceErrorToastMessage(result);
		        return RedirectToAction("Delete", new { model.ID });
	        }
		}

		private CustomerIndexViewModel GetIndexViewModel(CustomerIndexViewModel currModel)
		{
			currModel.FilterModel = currModel.FilterModel ?? new CustomerFilterModel();
			return currModel;
		}

		private CustomerCreateViewModel GetCreateViewModel(CustomerCreateViewModel currModel)
		{
			return currModel;
		}
	}
}
