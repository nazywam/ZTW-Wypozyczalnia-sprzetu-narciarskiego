using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Entities.FilterModels
{
	public class CustomerFilterModel
	{
		[StringLength(255)]
		[DisplayName("Imie")]
		public string S_FirstName { get; set; }

		[StringLength(255)]
		[DisplayName("Nazwisko")]
		public string S_LastName { get; set; }

		[StringLength(20)]
		[DisplayName("Numer dokumentu")]
		public string S_IdentyficationNumber { get; set; }

		public bool IsFiltered
		{
			get
			{
				if (S_FirstName !=  null || S_LastName != null || S_IdentyficationNumber != null)
					return true;
				else
					return false;
			}
		}
	}
}