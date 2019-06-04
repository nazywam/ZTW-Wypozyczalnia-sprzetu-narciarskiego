
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using i18n;
using SkiRent.Extensions;
namespace SkiRent.Controllers
{
	public abstract class BaseController : Controller
	{
		public  Toastr _Toastr { get; set; }
		protected const int PAGE_SIZE = 10;

		public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
		{
			return _Toastr.AddToastMessage(title, message, toastType);
		}

		public ToastMessage AddSuccessEditedToastMessage()
		{
			return _Toastr.AddToastMessage("[[[Success!]]]", "[[[Edited successfully.]]]", ToastType.Success);
		}
		public ToastMessage AddSuccessCreatedToastMessage()
		{
			return _Toastr.AddToastMessage("[[[Success!]]]", "[[[Added successfully.]]]", ToastType.Success);
		}
		public ToastMessage AddSuccessDeletedToastMessage()
		{
			return _Toastr.AddToastMessage("[[[Success!]]]", "[[[Deleted successfully.]]]", ToastType.Success);
		}
		public ToastMessage AddServiceErrorToastMessage(ServiceResult result)
		{
			return _Toastr.AddToastMessage("[[[Error!]]]", result.ErrorMessage, ToastType.Error);
		}

		protected override void OnException(ExceptionContext filterContext)
		{
			filterContext.ExceptionHandled = true;

			AddSuccessEditedToastMessage();
			//Redirect or return a view, but not both.
			filterContext.Result = RedirectToAction("Index", "Home");
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

//			if (Request.Cookies["currLang"] == null)
//			{
//				HttpCookie cookie = new HttpCookie("currLang");
//				cookie.Value = "pl";
//				cookie.Expires = DateTime.Now.AddDays(30);
//				Response.Cookies.Add(cookie);
//			}
		}

		[AllowAnonymous]
		public ActionResult SwitchLanguage(string lang)
		{
			LocalizedApplication.Current.DefaultLanguage = lang;
			HttpCookie cookie = new HttpCookie("currLang");
			cookie.Value = lang;
			cookie.Expires = DateTime.Now.AddDays(30);
			Response.Cookies.Add(cookie);

			return Redirect(Request.UrlReferrer.PathAndQuery);
		}
	}
}