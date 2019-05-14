using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using SkiRent.Entities.FilterModels;
using X.PagedList;

namespace SkiRent.ViewModels.Employee
{
	public class EmployeeIndexViewModel
	{
		public EmployeeFilterModel FilterModel { get; set; }
		public IPagedList<EmployeeBasicViewModel> EmployeeList;
	}
}