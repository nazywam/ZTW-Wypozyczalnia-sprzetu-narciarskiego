using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SkiRent.Entities.FilterModels;
using SkiRent.ViewModels.Item;
using X.PagedList;

namespace SkiRent.ViewModels.Order
{
	public class OrderIndexViewModel
	{
		public OrderFilterModel FilterModel { get; set; }
		public IPagedList<OrderBasicViewModel> OrderList { get; set; }
	}
}