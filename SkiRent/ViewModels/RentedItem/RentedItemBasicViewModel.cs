using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SkiRent.Entities;

using SkiRent.ViewModels.Item;
using SkiRent.ViewModels.Order;

namespace SkiRent.ViewModels.Payment
{
	public class RentedItemBasicViewModel
	{
		public int ID { get; set; }

		public int OrderID { get; set; }

		public int ItemID { get; set; }

		public virtual ItemBasicViewModel Item { get; set; }

		public virtual OrderBasicViewModel Order { get; set; }
	}
}