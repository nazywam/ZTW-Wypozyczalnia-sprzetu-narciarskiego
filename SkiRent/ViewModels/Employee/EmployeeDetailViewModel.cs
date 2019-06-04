using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Foolproof;
using SkiRent.Extensions;
using SkiRent.ViewModels.Order;

namespace SkiRent.ViewModels.Employee
{
	public class EmployeeDetailViewModel : EmployeeBasicViewModel
	{
		public virtual List<OrderBasicViewModel> Orders { get; set; }

		[RequiredIf("ID", Operator.EqualTo, 0, ErrorMessage = "[[[Password is required]]]")]
		[StringLength(30)]
		[DisplayName("[[[Password]]]")]
		public string Password { get; set; }
	}
}