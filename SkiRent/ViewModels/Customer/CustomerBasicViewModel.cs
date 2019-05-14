using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SkiRent.Entities;


namespace SkiRent.ViewModels.Customer
{
	public class CustomerBasicViewModel
	{
		public int ID { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("[[[Name]]]")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("[[[Surname]]]")]
		public string LastName { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("[[[Adress]]]")]
		public string Address { get; set; }

		[DisplayName("[[[Telephone number]]]")]
		public int PhoneNumber { get; set; }

		[Required]
		[StringLength(20)]
		[DisplayName("[[[Document number]]]")]
		public string IdentyficationNumber { get; set; }
	}
}