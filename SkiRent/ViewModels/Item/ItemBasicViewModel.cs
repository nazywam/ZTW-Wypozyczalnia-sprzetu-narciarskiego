using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using SkiRent.ViewModels.Category;

namespace SkiRent.ViewModels.Item
{
	public class ItemBasicViewModel
	{
		public int ID { get; set; }

		[Required]
		[DisplayName("Kategoria")]
		public string CategoryID { get; set; }

		[Required]
		[StringLength(30)]
		[DisplayName("Producent")]
		public string Manufacturer { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Model")]
		public string ModelName { get; set; }

		[Required]
		[StringLength(10)]
		[DisplayName("Rozmiar")]
		public string Size { get; set; }

		[StringLength(1)]
		public string Avaiable { get; set; }

		[DisplayName("Data zakupu")]
		public DateTime? Purchase_date { get; set; }

		public virtual CategoryBasicViewModel Category { get; set; }
		[DisplayName("Dostępny")]
		public bool IsAvaible
		{
			get { return Avaiable == "1"; }
			set { Avaiable = value ? "1" : "0"; }
		}
	}
}