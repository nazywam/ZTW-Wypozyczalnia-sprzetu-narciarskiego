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
		[DisplayName("[[[Category]]]")]
		public string CategoryID { get; set; }

		[Required]
		[StringLength(30)]
		[DisplayName("[[[Manufacturer]]]")]
		public string Manufacturer { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("[[[Model]]]")]
		public string ModelName { get; set; }

		[Required]
		[StringLength(10)]
		[DisplayName("[[[Size]]]")]
		public string Size { get; set; }

		[StringLength(1)]
		public string Avaiable { get; set; }

		[DisplayName("[[[Purchase date]]]")]
		public DateTime? Purchase_date { get; set; }

        [StringLength(255)]
        [DisplayName("[[[Barcode]]]")]
        public string Barcode { get; set; }

		public virtual CategoryBasicViewModel Category { get; set; }
		[DisplayName("[[[Available]]]")]
		public bool IsAvaible
		{
			get { return Avaiable == "1"; }
			set { Avaiable = value ? "1" : "0"; }
		}
	}
}