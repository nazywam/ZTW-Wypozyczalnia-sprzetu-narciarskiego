using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SkiRent.Authorization;
using SkiRent.Entities;
using SkiRent.Services;

namespace SkiRent
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie != null)
			{
				var empService = new EmployeeService(new Model());
				FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
				var identity = new User()
				{
					AuthenticationType = AuthenticationTypes.Basic,
					IsAuthenticated = true,
					Name = authTicket.Name,
					Employee = empService.Get(int.Parse(authTicket.Name))
				};
				var principal = new GenericPrincipal(identity, AuthorizationHelper.ConvertRolesStringToArray(authTicket.UserData));
				Context.User = principal;
			}
		}
	}
}
