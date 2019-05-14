using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkiRent.ViewModels.Item;


namespace SkiRent.ViewModels.Order
{
	public class OrderCreateViewModel
	{
		public OrderDetailViewModel Model { get; set; }
        public List<ItemBasicViewModel> ItemsFormList { get; set; }
        public List<SelectListItem> CustomerSelectList { get; set; }

        public bool IsEditMode { get; set; }
    }
}