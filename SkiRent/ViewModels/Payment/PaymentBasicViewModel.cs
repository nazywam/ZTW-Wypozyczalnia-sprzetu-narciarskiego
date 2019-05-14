using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SkiRent.Entities;

using SkiRent.ViewModels.Order;

namespace SkiRent.ViewModels.Payment
{
	public class PaymentBasicViewModel
	{
		public int ID { get; set; }

		public DateTime PaymentDate { get; set; }

		public decimal Amount { get; set; }

		[Required]
		[StringLength(3)]
		public string Currency { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public List<OrderBasicViewModel> Orders { get; set; }
	}
}