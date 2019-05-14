using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SkiRent.Entities;


namespace SkiRent.ViewModels.Category
{
	public class CategoryBasicViewModel
	{
		public int ID { get; set; }

		[Required]
		[StringLength(30)]
		[DisplayName("[[[Name]]]")]
		public string Name { get; set; }

		[DisplayName("[[[Price per day]]]")]
		public decimal? PricePerDay { get; set; }

		[DisplayName("[[[Parent category]]]")]
		public int? ParentCategoryID { get; set; }

		public virtual CategoryBasicViewModel ParentCategory { get; set; }
	}
}