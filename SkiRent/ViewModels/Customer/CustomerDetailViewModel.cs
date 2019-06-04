using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiRent.Entities;
using SkiRent.ViewModels.Order;

namespace SkiRent.ViewModels.Customer
{
	public class CustomerDetailViewModel : CustomerBasicViewModel
	{
		public virtual List<OrderBasicViewModel> Orders { get; set; }
	}
}