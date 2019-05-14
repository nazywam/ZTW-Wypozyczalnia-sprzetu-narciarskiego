using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SkiRent.Entities;

using SkiRent.ViewModels.Customer;
using SkiRent.ViewModels.Employee;
using SkiRent.ViewModels.Payment;

namespace SkiRent.ViewModels.Order
{
	public class OrderBasicViewModel
	{
        [DisplayName("[[[Order number]]]")]
        public int ID { get; set; }

        [DisplayName("[[[Employee]]]")]
        public int EmployeeID { get; set; }

        [DisplayName("[[[Customer]]]")]
        public int CustomerID { get; set; }

		//[Column(TypeName = "timestamp")]
        [DisplayName("[[[Date rented]]]")]
		public DateTime? Date_Rented { get; set; }

        //[Column(TypeName = "timestamp")]
        [DisplayName("[[[Date return]]]")]
        public DateTime? Date_Return { get; set; }

		[StringLength(255)]
        [DisplayName("[[[Comment]]]")]
        public string Comment { get; set; }

		public virtual CustomerBasicViewModel Customer { get; set; }

		public virtual EmployeeBasicViewModel Employee { get; set; }

        [DisplayName("[[[Returned]]]")]
        public bool IsReturned
        {
            get { return Date_Return == null ? false : true; }
        }
    }
}