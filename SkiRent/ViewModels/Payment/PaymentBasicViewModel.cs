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
        [DefaultValue("PLN")]
		public string Currency { get; set; }

        [DefaultValue(1)]
        public decimal ExchangeRate { get; set; }

        public decimal AmountInPLN
        {
            get { return Amount * ExchangeRate; }
        }
        public  OrderBasicViewModel Order { get; set; }
    }
}