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
		[DisplayName("Imię")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Nazwisko")]
		public string LastName { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Adres")]
		public string Address { get; set; }

		[DisplayName("Numer telefonu")]
		public int PhoneNumber { get; set; }

		[Required]
		[StringLength(20)]
		[DisplayName("Numer dokumentu")]
		public string IdentyficationNumber { get; set; }
	}
}