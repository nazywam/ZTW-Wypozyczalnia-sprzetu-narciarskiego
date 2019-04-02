using System.Web;
using System.Web.Mvc;
using SkiRent.Extensions;

namespace SkiRent
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new MessagesActionFilter());
		}
	}
}
