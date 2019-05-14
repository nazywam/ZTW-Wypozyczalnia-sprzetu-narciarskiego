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
		[DisplayName("Nazwa")]
		public string Name { get; set; }

		[Required]
		[DisplayName("Cena za dzień")]
		public decimal? PricePerDay { get; set; }

		[DisplayName("Kategoria nadrzędna")]
		public int? ParentCategoryID { get; set; }

		public virtual CategoryBasicViewModel ParentCategory { get; set; }
	}
}