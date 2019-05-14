using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using AutoMapper;
using SkiRent.Authorization;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.ViewModels.Employee;

namespace SkiRent.Services
{
	public class EmployeeService : BaseService<Model>
	{
	    private readonly IMapper _mapper;

        public EmployeeService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }
			
		public EmployeeBasicViewModel GetUserByUserNameAndPassword(string login, string password)
		{
		    string hashedPass = AuthorizationHelper.HashPassword(login, password);
            var employee = m_Context.Employees.SingleOrDefault(emp => emp.Login == login && emp.Password == hashedPass);
		    return _mapper.Map<EmployeeBasicViewModel>(employee);
		}

		public List<EmployeeBasicViewModel> Filter(EmployeeFilterModel model)
		{
			var items = m_Context.Employees.AsQueryable();
			if (model != null)
			{
				if (!string.IsNullOrEmpty(model.S_FirstName))
					items = items.Where(x => x.FirstName.Contains(model.S_FirstName));
				if (!string.IsNullOrEmpty(model.S_LastName))
					items = items.Where(x => x.LastName.Contains(model.S_LastName));
				if (!string.IsNullOrEmpty(model.S_Login))
					items = items.Where(x => x.Login.Contains(model.S_Login));
			}
			return _mapper.Map<List<EmployeeBasicViewModel>>(items.ToList());
		}

		public List<EmployeeBasicViewModel> GetAll()
		{
		    var employees = m_Context.Employees.ToList();
		    return _mapper.Map<List<EmployeeBasicViewModel>>(employees);
		}

        public EmployeeDetailViewModel Get(int id)
        {
            var employee = m_Context.Employees.SingleOrDefault(emp => emp.ID == id);
            return _mapper.Map<EmployeeDetailViewModel>(employee);
        }

		public ServiceResult Update(EmployeeDetailViewModel item)
		{
		    var employee = m_Context.Employees.SingleOrDefault(emp => emp.ID == item.ID);
            if (employee != null)
            {
	            string tmp_pass = employee.Password;
	            _mapper.Map(item, employee);
				if(employee.Password!=tmp_pass)
					employee.Password = AuthorizationHelper.HashPassword(employee.Login, employee.Password);
				return SaveChanges();
            }
			return new ServiceResult(false, "");
		}

		public ServiceResult Delete(EmployeeDetailViewModel item)
		{
		    var employee = m_Context.Employees.SingleOrDefault(emp => emp.ID == item.ID);
            if (employee != null)
			{
				m_Context.Employees.Remove(employee);
				return SaveChanges();
			}
            return new ServiceResult(false, "");
		}

		public ServiceResult Add(EmployeeDetailViewModel item)
		{
		    var employee = _mapper.Map<Employee>(item);
            employee.Password = AuthorizationHelper.HashPassword(employee.Login, employee.Password);
            m_Context.Employees.Add(employee);
            return SaveChanges();
		}
	}
}