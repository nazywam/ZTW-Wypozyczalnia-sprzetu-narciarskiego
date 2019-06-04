using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiRent.ViewModels.Payment;


namespace SkiRent.ViewModels.Item
{
	public class ItemDetailViewModel : ItemBasicViewModel
	{
		public virtual List<RentedItemBasicViewModel> Rented_Items { get; set; }
	}
}