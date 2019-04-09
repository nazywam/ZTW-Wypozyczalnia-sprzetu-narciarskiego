
using System.Web.Mvc;
using SkiRent.Extensions;
using MvcBreadCrumbs;

namespace SkiRent.Controllers
{
	public abstract class BaseController : Controller
	{
		public  Toastr _Toastr { get; set; }
		protected const int PAGE_SIZE = 5;

		public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
		{
			return _Toastr.AddToastMessage(title, message, toastType);
		}

		public ToastMessage AddSuccessEditedToastMessage()
		{
			return _Toastr.AddToastMessage("Sukces!", "Pomyślnie zeedytowano.", ToastType.Success);
		}
		public ToastMessage AddSuccessCreatedToastMessage()
		{
			return _Toastr.AddToastMessage("Sukces!", "Pomyślnie dodano.", ToastType.Success);
		}
		public ToastMessage AddSuccessDeletedToastMessage()
		{
			return _Toastr.AddToastMessage("Sukces!", "Pomyślnie usunięto.", ToastType.Success);
		}
	}
}