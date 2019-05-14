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
using SkiRent.ViewModels.Payment;

namespace SkiRent.Services
{
	public class PaymentService : BaseService<Model>
	{
	    private readonly IMapper _mapper;
	    private const string TREE_SEPARATOR = "-";

        public PaymentService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }

//		public List<PaymentBasicViewModel> Filter(PaymentFilterModel model)
//		{
//			var items = m_Context.Payments.AsQueryable();
//			if (model != null)
//			{
//				if (!string.IsNullOrEmpty(model.S_Name))
//					items = items.Where(x => x.Name.Contains(model.S_Name));
//				if (model.S_PricePerDay != 0)
//					items = items.Where(x => x.PricePerDay == model.S_PricePerDay);
//			}
//			return _mapper.Map<List<PaymentBasicViewModel>>(items.ToList());
//		}

		public List<PaymentBasicViewModel> GetAll()
		{
			var Items = m_Context.Payments.ToList();
			return _mapper.Map<List<PaymentBasicViewModel>>(Items);
		}

		public PaymentBasicViewModel Get(int id)
		{
			var v_item = m_Context.Payments.SingleOrDefault(emp => emp.ID == id);
			var tmp = _mapper.Map<PaymentBasicViewModel>(v_item);
			return tmp;
		}

		public ServiceResult Update(PaymentBasicViewModel item)
		{
			var v_item = m_Context.Payments.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				_mapper.Map(item, v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Delete(PaymentBasicViewModel item)
		{
			var v_item = m_Context.Payments.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				m_Context.Payments.Remove(v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Add(PaymentBasicViewModel item)
		{
			var v_item = _mapper.Map<Payment>(item);
			m_Context.Payments.Add(v_item);
			return SaveChanges();
		}
	}
}