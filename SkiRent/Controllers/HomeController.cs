using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SkiRent.Authorization;
using SkiRent.Entities;
using SkiRent.Entities.DTO;
using SkiRent.Extensions;
using SkiRent.Services;
using SkiRent.ViewModels;
using MvcBreadCrumbs;

namespace SkiRent.Controllers
{
	public class HomeController : BaseController
	{
		[BreadCrumb(Clear = true, Label = "Strona główna")]
		public ActionResult Index()
		{
			return View();
		}
	}
}