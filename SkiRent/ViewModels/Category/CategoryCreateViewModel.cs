using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SkiRent.ViewModels.Category
{
	public class CategoryCreateViewModel
	{
		public CategoryDetailViewModel Model { get; set; }
		public List<SelectListItem> CategorySelectList { get; set; }
	}
}