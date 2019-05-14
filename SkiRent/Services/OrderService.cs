using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.ViewModels.Item;
using SkiRent.ViewModels.Order;
using SkiRent.ViewModels.Payment;

namespace SkiRent.Services
{
	public class OrderService : BaseService<Model>
	{
	    private readonly IMapper _mapper;
        private CategoryService _categoryService;

        public OrderService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
            _categoryService = new CategoryService(context);
        }

		public List<OrderBasicViewModel> Filter(OrderFilterModel model)
		{
			var items = m_Context.Orders.AsQueryable();
			if (model != null)
			{
                if (!string.IsNullOrEmpty(model.S_Customer_ID))
                {
                    int CategoryID = Int32.Parse(model.S_Customer_ID);
                    items = items.Where(x => x.CustomerID == CategoryID);
                }

                if (!string.IsNullOrEmpty(model.S_Returned))
                {
                    bool castedInt = model.S_Returned.Equals("0") ? false : true;
                    if (!castedInt)
                        items = items.Where(x => x.Date_Return == null);
                    else
                        items = items.Where(x => x.Date_Return != null);
                }

                if (model.S_ID != null)
                    items = items.Where(x => x.ID == model.S_ID);
            }
			return _mapper.Map<List<OrderBasicViewModel>>(items.ToList());
		}

		public List<OrderBasicViewModel> GetAll()
		{
			var Orders = m_Context.Orders.ToList();
			return _mapper.Map<List<OrderBasicViewModel>>(Orders);
		}

		public OrderDetailViewModel Get(int id)
		{
			var v_item = m_Context.Orders.SingleOrDefault(emp => emp.ID == id);
			var tmp = _mapper.Map<OrderDetailViewModel>(v_item);
            if (tmp.Date_Return != null)
                tmp.TimeRented = tmp.Date_Return.Value - tmp.Date_Rented.Value;
            else
                tmp.TimeRented = DateTime.Now - tmp.Date_Rented.Value;

            var daysToPay = (int)Math.Ceiling(tmp.TimeRented.TotalHours / 24);
            tmp.DaysRented = daysToPay;

            foreach (RentedItemBasicViewModel item in tmp.Rented_Items)
            {
                var itemPricePerDay = _categoryService.GetCategoryPricePerDay(int.Parse(item.Item.CategoryID));
                tmp.PricePerDay += itemPricePerDay;
                tmp.PaymentValue += daysToPay * itemPricePerDay;
            }

            return tmp;
        }

		public ServiceResult Update(OrderDetailViewModel item)
		{
			var v_item = m_Context.Orders.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				_mapper.Map(item, v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

        public ServiceResult Update(OrderCreateViewModel createModel)
        {
            var v_item = m_Context.Orders.SingleOrDefault(emp => emp.ID == createModel.Model.ID);
            if (v_item != null)
            {
                ConvertCreateModelToBasicModel(createModel, v_item);
                _mapper.Map(createModel.Model, v_item);
                return SaveChanges();
            }
            return new ServiceResult(false, "");
        }

        private void ConvertCreateModelToBasicModel(OrderCreateViewModel createModel, Order orderEntity)
        {
            if (createModel.ItemsFormList == null)
                createModel.ItemsFormList = new List<ItemBasicViewModel>();

            var selectedItems = createModel.ItemsFormList.Select(i => i.ID).ToList();

            var orderItems = orderEntity.Rented_Items.Select(i => i.ItemID).ToList();

            foreach (int orderItemsID in orderItems)
            {
                if (!selectedItems.Contains(orderItemsID))
                {
                    m_Context.RentedItems.Remove(orderEntity.Rented_Items.Where(i => i.ItemID == orderItemsID).FirstOrDefault());
                    m_Context.Items.Where(i => i.ID == orderItemsID).FirstOrDefault().Avaiable = "1";
                }
            }
            foreach (ItemBasicViewModel selectedItem in createModel.ItemsFormList)
            {
                if (!orderItems.Contains(selectedItem.ID))
                {
                    orderEntity.Rented_Items.Add(new RentedItem()
                    {
                        ItemID = selectedItem.ID,
                        OrderID = orderEntity.ID
                    });
                    m_Context.Items.Where(i => i.ID == selectedItem.ID).FirstOrDefault().Avaiable = "0";
                }
            }
        }

        public ServiceResult Delete(OrderDetailViewModel item)
		{
			var v_item = m_Context.Orders.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				m_Context.Orders.Remove(v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Add(OrderDetailViewModel item)
		{
			var v_item = _mapper.Map<Order>(item);
            foreach (RentedItem rentedItem in v_item.Rented_Items)
                rentedItem.Item.Avaiable = "1";
			m_Context.Orders.Add(v_item);
			return SaveChanges();
		}

        public ServiceResult Add(OrderCreateViewModel createModel)
        {
            var v_item = _mapper.Map<Order>(createModel.Model);
            m_Context.Orders.Add(v_item);
            ConvertCreateModelToBasicModel(createModel, v_item);
            v_item.Date_Rented = DateTime.Now;
            return SaveChanges();
        }
    }
}