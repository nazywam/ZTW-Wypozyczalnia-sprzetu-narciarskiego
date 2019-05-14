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
        public int OrderID { get; set; }

        public DateTime PaymentDate { get; set; }

		public decimal Amount { get; set; }

		[Required]
		[StringLength(3)]
		public string Currency { get; set; }

        public decimal ExchangeRate { get; set; }
        public  OrderBasicViewModel Order { get; set; }
    }
}