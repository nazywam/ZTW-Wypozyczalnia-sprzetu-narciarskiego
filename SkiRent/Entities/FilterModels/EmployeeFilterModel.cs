using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.Entities.FilterModels
{
	public class EmployeeFilterModel
	{
		[StringLength(30)]
		[DisplayName("Imię")]
		public string S_FirstName { get; set; }
		[StringLength(30)]
		[DisplayName("Nazwisko")]
		public string S_LastName { get; set; }
		[StringLength(30)]
		[DisplayName("Login")]
		public string S_Login { get; set; }

		public bool IsFiltered
		{
			get
			{
				if (S_FirstName != null || S_LastName != null || S_Login != null)
					return true;
				else
					return false;
			}
		}
	}
}