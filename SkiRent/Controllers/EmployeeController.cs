using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
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

namespace SkiRent.Controllers
{
	[Authorize(Roles = "Admin")]
    public class EmployeeController : BaseController
    {
	    private EmployeeService _employeeService;

	    public EmployeeController()
	    {
			_employeeService = new EmployeeService(new Model());
	    }

	    [MvcSiteMapNode(Title = "[[[Employee management]]]", ParentKey = "Home_Index", Key="Employee_Index")]
		public ActionResult Index(int? page)
        {
	        EmployeeIndexViewModel model = new EmployeeIndexViewModel()
            {
                EmployeeList = _employeeService.GetAll().ToPagedList(page ?? 1, PAGE_SIZE),
            };

            return View(GetIndexViewModel(model));
        }

		[HttpPost]
		public ActionResult Index(EmployeeFilterModel filterModel)
        {
	        EmployeeIndexViewModel model = new EmployeeIndexViewModel()
	        {
		        EmployeeList = filterModel.IsFiltered ? _employeeService.Filter(filterModel).ToPagedList() : _employeeService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel
	        };

			return View(GetIndexViewModel(model));
        }

		[MvcSiteMapNode(Title = "[[[Details]]]", ParentKey = "Employee_Index", Key = "Employee_Details", PreservedRouteParameters = "id")]
		public ActionResult Details(int id)
        {
            return View(_employeeService.Get(id));
        }

		[MvcSiteMapNode(Title = "[[[Create]]]", ParentKey = "Employee_Index", Key = "Employee_Create")]
		public ActionResult Create()
        {
            return View(new EmployeeDetailViewModel());
        }

        [HttpPost]
		public ActionResult Create(EmployeeDetailViewModel model)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _employeeService.Add(model);
				if (result.Status)
		        {
			        AddSuccessCreatedToastMessage();
			        return RedirectToAction("Index");
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
			        return View(model);
		        }
	        }
			return View(model);
        }

		[MvcSiteMapNode(Title = "[[[Edit]]]", ParentKey = "Employee_Index", Key = "Employee_Edit", PreservedRouteParameters = "id")]
		public ActionResult Edit(int id)
		{
            return View("Create", _employeeService.Get(id));
        }

        [HttpPost]
		public ActionResult Edit(EmployeeDetailViewModel model)
        {
			if (ModelState.IsValid)
	        {
				ServiceResult result = _employeeService.Update(model);
				if (result.Status)
		        {
			        AddSuccessEditedToastMessage();
			        return RedirectToAction("Index");
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
			        return View("Create",model);
		        }
			}
		    return View("Create",model);
		}

		[MvcSiteMapNode(Title = "[[[Delete]]]", ParentKey = "Employee_Index", Key = "Employee_Delete", PreservedRouteParameters = "id")]
		public ActionResult Delete(int id)
        {
            return View(_employeeService.Get(id));
        }

        [HttpPost]
		public ActionResult Delete(EmployeeDetailViewModel model)
        {
	        ServiceResult result = _employeeService.Delete(model);
	        if (result.Status)
	        {
		        AddSuccessDeletedToastMessage();
		        return RedirectToAction("Index");
			}
	        else
	        {
		        AddServiceErrorToastMessage(result);
		        return RedirectToAction("Delete", new {model.ID});
			}
		}

		private EmployeeIndexViewModel GetIndexViewModel(EmployeeIndexViewModel currModel)
		{
			currModel.FilterModel = currModel.FilterModel ?? new EmployeeFilterModel();
			return currModel;
		}
	}
}
