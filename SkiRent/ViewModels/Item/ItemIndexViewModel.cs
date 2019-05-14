using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SkiRent.Entities.FilterModels;
using X.PagedList;

namespace SkiRent.ViewModels.Item
{
	public class ItemIndexViewModel
	{
		public ItemFilterModel FilterModel { get; set; }
		public List<SelectListItem> CategorySelectList { get; set; }
		public List<SelectListItem> AvaibleSelectList { get; set; }
		public IPagedList<ItemBasicViewModel> ItemList { get; set; }
	}
}