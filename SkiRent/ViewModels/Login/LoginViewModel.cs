using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkiRent.ViewModels.Login
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "[[[Login is required]]].")]
		[DisplayName("[[[Login]]]")]
		public string Login { get; set; }
		[Required(ErrorMessage = "[[[Password is required]]].")]
		[DisplayName("[[[Password]]]")]
		public string Password { get; set; }
	}
}