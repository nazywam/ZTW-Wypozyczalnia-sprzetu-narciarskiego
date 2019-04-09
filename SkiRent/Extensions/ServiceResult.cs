using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkiRent.Extensions
{
	public class ServiceResult
	{
		public bool Status { get; set; }
		public string ErrorMessage { get; set; }

		public ServiceResult(bool status, string errorMessage = "")
		{
			Status = status;
			ErrorMessage = errorMessage;
		}
	}
}