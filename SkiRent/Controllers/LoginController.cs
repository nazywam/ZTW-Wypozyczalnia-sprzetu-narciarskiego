﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SkiRent.Authorization;
using SkiRent.Entities;
using SkiRent.Extensions;
using SkiRent.ViewModels.Login;


namespace SkiRent.Controllers
{
    public class LoginController : BaseController
    {
		public ActionResult Index()
	    {
		    return RedirectToAction("Login");
	    }

	    public ActionResult Redirect()
	    {
		    if (HttpContext.User.Identity.IsAuthenticated)
		    {
			    AddToastMessage("[[[Access denied.]]]", "[[[You don't have required permissions.]]]", ToastType.Warning);
			}
		    else
		    {
			    AddToastMessage("[[[Access denied.]]]", "[[[You need to log in first.]]]", ToastType.Warning);
			}
		    return RedirectToAction("Login");
		}

		public ActionResult Login()
        {
            return View("Index", new LoginViewModel());
        }

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
        {
	        if (ModelState.IsValid)
	        {
		        var tmp = new AuthorizationHelper();
		        if (tmp.ValidateUser(model.Login, model.Password))
		        {
			        AddToastMessage("[[[Logged in successfully]]]", "[[[Login successful]]]", ToastType.Success);
			        return RedirectToAction("Index", "Home");
				}
		        else
		        {
			        AddToastMessage("[[[Login Fail]]]", "[[[Login or password is incorrect]]]", ToastType.Error);
		        }
			}

	        return View("Index", model);
        }

		public ActionResult Logout()
        {
	        FormsAuthentication.SignOut();
	        return RedirectToAction("Index", "Home");
        }
	}
}
