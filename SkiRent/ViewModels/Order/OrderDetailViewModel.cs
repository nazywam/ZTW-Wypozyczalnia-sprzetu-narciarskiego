using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SkiRent.Entities;
using SkiRent.ViewModels.Item;
using SkiRent.ViewModels.Payment;

namespace SkiRent.ViewModels.Order
{
	public class OrderDetailViewModel : OrderBasicViewModel
	{
		public List<RentedItemBasicViewModel> Rented_Items { get; set; }
        public List<PaymentBasicViewModel> Payments { get; set; }
        [DisplayName("[[[Time rented]]]")]
        public TimeSpan TimeRented { get; set; }
        [DisplayName("[[[Days Rented]]]")]
        public int DaysRented { get; set; }
        [DisplayName("[[[Price per day]]]")]
        public decimal PricePerDay { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("[[[Order value]]]")]
        public decimal OrderValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("[[[Payments value]]]")]
        public decimal PaymentsValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00000000000000000}", ApplyFormatInEditMode = true)]
        [DisplayName("[[[Rest to pay]]]")]
        public decimal RestToPay { get; set; }
    }
}