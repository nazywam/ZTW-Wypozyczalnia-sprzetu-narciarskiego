using System;
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
			    AddToastMessage("Dostęp zabroniony.", "Nie posiadasz wystarczających uprawnień.", ToastType.Warning);
			}
		    else
		    {
			    AddToastMessage("Dostęp zabroniony.", "Musisz się zalogować.", ToastType.Warning);
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
			        AddToastMessage("Pomyślnie zalogowano", "Udało się zalogować", ToastType.Success);
			        return RedirectToAction("Index", "Home");
				}
		        else
		        {
			        AddToastMessage("Nieudana próba logowania", "Hasło lub login jest niepoprawne", ToastType.Error);
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