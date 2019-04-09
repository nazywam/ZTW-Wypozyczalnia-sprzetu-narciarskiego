using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkiRent.Entities;
using SkiRent.Entities.DTO;
using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using SkiRent.ViewModels.Employee;
using X.PagedList;
using MvcBreadCrumbs;
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

		[BreadCrumb(Clear = true, Label = "Zarządzanie pracownikiami")]
        public ActionResult Index(int? page)
        {
            EmployeeIndexViewModel model = new EmployeeIndexViewModel()
            {
                EmployeeList = _employeeService.GetAll().ToPagedList(page ?? 1, PAGE_SIZE),
                FilterModel = new EmployeeFilterModel()
            };

            return View(model);
        }

        [HttpPost]
		public ActionResult Index(EmployeeFilterModel filterModel)
        {
	        EmployeeIndexViewModel model = new EmployeeIndexViewModel()
	        {
		        EmployeeList = filterModel.IsFiltered ? _employeeService.Filter(filterModel).ToPagedList() : _employeeService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel
	        };

			return View(model);
        }

		[BreadCrumb(Label = "Szczegóły")]
		public ActionResult Details(int id)
        {
            return View(_employeeService.Get(id));
        }

		[BreadCrumb(Label = "Dodaj")]
		public ActionResult Create()
        {
            return View(new EmployeeDTO());
        }

        [HttpPost]
		public ActionResult Create(EmployeeDTO model)
        {
	        ServiceResult result = _employeeService.Add(model);
	        if (ModelState.IsValid)
	        {
		        if (result.Status)
		        {
			        AddSuccessEditedToastMessage();
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

		[BreadCrumb(Label = "Edytuj")]
		public ActionResult Edit(int id)
		{
			var x = _employeeService.Get(id);
            return View("Create", _employeeService.Get(id));
        }

        [HttpPost]
		public ActionResult Edit(EmployeeDTO model)
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

		[BreadCrumb(Label = "Usuń")]
		public ActionResult Delete(int id)
        {
            return View(_employeeService.Get(id));
        }

        [HttpPost]
		public ActionResult Delete(EmployeeDTO model)
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
    }
}
