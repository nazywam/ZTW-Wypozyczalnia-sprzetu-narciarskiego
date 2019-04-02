using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkiRent.Entities;
using SkiRent.Entities.DTO;

namespace SkiRent.ViewModels
{
	public class IndexViewModel
	{
		public List<EmployeeDTO> Employeeses { get; set; }
		public List<Item> Items { get; set; }
		public List<Customer> Customers { get; set; }
	}
}