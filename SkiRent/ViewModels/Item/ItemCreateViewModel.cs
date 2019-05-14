using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SkiRent.ViewModels.Item
{
	public class ItemCreateViewModel
	{
		public ItemDetailViewModel Model { get; set; }
		public List<SelectListItem> CategorySelectList { get; set; }
	}
}