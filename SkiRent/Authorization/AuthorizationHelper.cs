using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using SkiRent.Entities;
using SkiRent.Services;

namespace SkiRent.Authorization
{
	public class AuthorizationHelper
	{
		private const string SEPARATOR = "|";
		private const string SOIL = "@#$%^&*()";
		public bool ValidateUser(string login, string password)
		{
			EmployeeService employeeService = new EmployeeService(new Model());

			var user = employeeService.GetUserByUserNameAndPassword(login, password);
			if (user == null)
			{
				return false;
			}
			else
			{
				string userData = user.PermissionLevel == 1 ? "Admin" : "";
				var authTicket = new FormsAuthenticationTicket(1, user.ID +"", DateTime.Now,
					DateTime.Now.AddMinutes(30), true, userData);

				string cookieContents = FormsAuthentication.Encrypt(authTicket);
				var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
				{
					Expires = authTicket.Expiration,
					Path = FormsAuthentication.FormsCookiePath
				};
				if (HttpContext.Current != null)
				{
					HttpContext.Current.Response.Cookies.Add(cookie);
				}
				return true;
			}
		}

		public static String HashPassword(string login, string password)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(login+SOIL+password);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}

		public static String[] ConvertRolesStringToArray(string roles)
		{
			return roles.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}