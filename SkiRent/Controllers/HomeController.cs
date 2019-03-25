using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkiRent.Entities;
using SkiRent.ViewModels;

namespace SkiRent.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var model = new Model();
			return View(new IndexViewModel()
			{
				Employeeses = model.Employees.ToList(),
				Customers =	model.Customers.ToList(),
				Items =  model.Items.ToList()
			});
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}