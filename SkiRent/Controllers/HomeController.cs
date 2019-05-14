using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSiteMapProvider;
using SkiRent.Authorization;
using SkiRent.Entities;

using SkiRent.Extensions;
using SkiRent.Services;
using SkiRent.ViewModels;


namespace SkiRent.Controllers
{
	public class HomeController : BaseController
	{
		[MvcSiteMapNode(Title = "[[[Home]]]", Key = "Home_Index")]
		public ActionResult Index()
		{
			return View();
		}
	}
}