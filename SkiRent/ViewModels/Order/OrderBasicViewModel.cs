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
		public int ID { get; set; }

		public int PaymentID { get; set; }

		public int EmployeeID { get; set; }

		public int CustomerID { get; set; }

		//[Column(TypeName = "timestamp")]
		public DateTime? Date_Rented { get; set; }

		//[Column(TypeName = "timestamp")]
		public DateTime? Date_Return { get; set; }

		[StringLength(255)]
		public string Comment { get; set; }

		public virtual CustomerBasicViewModel Customer { get; set; }

		public virtual EmployeeBasicViewModel Employee { get; set; }

		public virtual PaymentBasicViewModel Payment { get; set; }

		public decimal Value { get; set; }
	}
}