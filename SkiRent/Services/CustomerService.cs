using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.ViewModels.Customer;
using SkiRent.ViewModels.Item;

namespace SkiRent.Services
{
	public class CustomerService : BaseService<Model>
	{
	    private readonly IMapper _mapper;

        public CustomerService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }

		public List<CustomerBasicViewModel> Filter(CustomerFilterModel model)
		{
			var items = m_Context.Customers.AsQueryable();
			if (model != null)
			{
				if (!string.IsNullOrEmpty(model.S_FirstName))
					items = items.Where(x => x.FirstName.Contains(model.S_FirstName));
				if (!string.IsNullOrEmpty(model.S_LastName))
					items = items.Where(x => x.LastName.Contains(model.S_LastName));
				if (!string.IsNullOrEmpty(model.S_IdentyficationNumber))
					items = items.Where(x => x.IdentyficationNumber.Contains(model.S_IdentyficationNumber));
			}
			return _mapper.Map<List<CustomerBasicViewModel>>(items.ToList());
		}

		public List<CustomerBasicViewModel> GetAll()
		{
			var Items = m_Context.Customers.ToList();
			return _mapper.Map<List<CustomerBasicViewModel>>(Items);
		}

		public CustomerDetailViewModel Get(int id)
		{
			var v_item = m_Context.Customers.SingleOrDefault(emp => emp.ID == id);
			var tmp = _mapper.Map<CustomerDetailViewModel>(v_item);
			return tmp;
		}

		public ServiceResult Update(CustomerDetailViewModel item)
		{
			var v_item = m_Context.Customers.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				_mapper.Map(item, v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Delete(CustomerDetailViewModel item)
		{
			var v_item = m_Context.Customers.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				m_Context.Customers.Remove(v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Add(CustomerDetailViewModel item)
		{
			var v_item = _mapper.Map<Customer>(item);
			m_Context.Customers.Add(v_item);
			return SaveChanges();
		}

        public List<SelectListItem> GetSelectCustomerList()
        {
            var categories = m_Context.Customers.ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (Customer cut in categories)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = string.Format("{0} {1} - {2}", cut.FirstName, cut.LastName, cut.IdentyficationNumber),
                    Value = cut.ID+""
                });
            }
            return listItems;
        }
    }
}