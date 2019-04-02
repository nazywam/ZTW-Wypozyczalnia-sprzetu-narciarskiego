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
using SkiRent.Entities.DTO;
using SkiRent.Entities.FilterModels;

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

		public void Update(EmployeeDTO item)
		{

		}

		public void Delete(EmployeeDTO item)
		{

		}

		public void Add(EmployeeDTO item)
		{

		}
	}
}