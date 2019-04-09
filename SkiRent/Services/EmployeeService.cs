﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using AutoMapper;
using SkiRent.Authorization;
using SkiRent.Entities;
using SkiRent.Entities.DTO;
using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;

namespace SkiRent.Services
{
	public class EmployeeService : BaseService<Model>, IService<EmployeeDTO>
	{
	    private readonly IMapper _mapper;

        public EmployeeService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }
			
		public EmployeeDTO GetUserByUserNameAndPassword(string login, string password)
		{
		    string hashedPass = AuthorizationHelper.HashPassword(login, password);
            var employee = m_Context.Employees.SingleOrDefault(emp => emp.Login == login && emp.Password == hashedPass);
		    return _mapper.Map<EmployeeDTO>(employee);
		}

		public List<EmployeeDTO> Filter(EmployeeFilterModel model)
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
			return _mapper.Map<List<EmployeeDTO>>(items.ToList());
		}

		public List<EmployeeDTO> GetAll()
		{
		    var employees = m_Context.Employees.ToList();
		    return _mapper.Map<List<EmployeeDTO>>(employees);
		}

        public EmployeeDTO Get(int id)
        {
            var employee = m_Context.Employees.SingleOrDefault(emp => emp.ID == id);
            return _mapper.Map<EmployeeDTO>(employee);
        }

		public ServiceResult Update(EmployeeDTO item)
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

		public ServiceResult Delete(EmployeeDTO item)
		{
		    var employee = m_Context.Employees.SingleOrDefault(emp => emp.ID == item.ID);
            if (employee != null)
			{
				m_Context.Employees.Remove(employee);
				return SaveChanges();
			}
            return new ServiceResult(false, "");
		}

		public ServiceResult Add(EmployeeDTO item)
		{
		    var employee = _mapper.Map<Employee>(item);
            employee.Password = AuthorizationHelper.HashPassword(employee.Login, employee.Password);
            m_Context.Employees.Add(employee);
            return SaveChanges();
		}
	}
}