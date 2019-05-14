using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Entities.FilterModels
{
	public class CategoryFilterModel
	{
		[Required]
		[StringLength(30)]
		[DisplayName("Nazwa")]
		public string S_Name { get; set; }

		[Required]
		[DisplayName("Cena za dzień")]
		public decimal? S_PricePerDay { get; set; }

		public bool IsFiltered
		{
			get
			{
				if (S_Name != null || S_PricePerDay != null)
					return true;
				else
					return false;
			}
		}
	}
}