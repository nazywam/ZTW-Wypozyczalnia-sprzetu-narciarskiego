using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using SkiRent.Entities;
using SkiRent.Entities.DTO;

namespace SkiRent.Authorization
{
	public class User : IIdentity
	{
		public string Name { get; set; }
		public string AuthenticationType { get; set; }
		public bool IsAuthenticated { get; set; }
		public EmployeeDTO Employee { get; set; }
	}
}