using System;
using System.Collections.Generic;
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
		public ActionResult Index()
        {
			EmployeeIndexViewModel model = new EmployeeIndexViewModel()
			{
				EmployeeList = _employeeService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = new EmployeeFilterModel()
			};

			return View(model);
        }

        [HttpPost]
		public ActionResult Index(EmployeeFilterModel filterModel)
        {
	        EmployeeIndexViewModel model = new EmployeeIndexViewModel()
	        {
		        EmployeeList = _employeeService.Filter(filterModel).ToPagedList(),
				FilterModel = filterModel
	        };

			return View(model);
        }
    }
}
