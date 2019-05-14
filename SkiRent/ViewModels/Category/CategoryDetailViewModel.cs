using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiRent.Entities;

using SkiRent.ViewModels.Item;

namespace SkiRent.ViewModels.Category
{
	public class CategoryDetailViewModel : CategoryBasicViewModel
	{
		public List<ItemBasicViewModel> ItemList { get; set; }
		public List<CategoryBasicViewModel> SubCategories { get; set; }
	}
}