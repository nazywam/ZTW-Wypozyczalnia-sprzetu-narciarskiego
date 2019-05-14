using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiRent.Entities;
using SkiRent.ViewModels.Item;
using SkiRent.ViewModels.Payment;

namespace SkiRent.ViewModels.Order
{
	public class OrderDetailViewModel : OrderBasicViewModel
	{
		public List<RentedItemBasicViewModel> Rented_Items { get; set; }
	}
}