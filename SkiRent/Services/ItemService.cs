using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using SkiRent.Authorization;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.ViewModels.Item;

namespace SkiRent.Services
{
	public class ItemService : BaseService<Model>
	{
	    private readonly IMapper _mapper;

        public ItemService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }
			
		public List<ItemBasicViewModel> Filter(ItemFilterModel model)
		{
			var items = m_Context.Items.AsQueryable();
			if (model != null)
			{
				if (!string.IsNullOrEmpty(model.S_Manufacturer))
					items = items.Where(x => x.Manufacturer.Contains(model.S_Manufacturer));
				if (!string.IsNullOrEmpty(model.S_Model))
					items = items.Where(x => x.ModelName.Contains(model.S_Model));
				if (!string.IsNullOrEmpty(model.S_Avaible))
					items = items.Where(x => x.Avaiable.Contains(model.S_Avaible));
				if (!string.IsNullOrEmpty(model.S_CategoryID))
				{
					int CategoryID = Int32.Parse(model.S_CategoryID);
					items = items.Where(x => x.CategoryID == CategoryID);
				}
			}
			return _mapper.Map<List<ItemBasicViewModel>>(items.ToList());
		}

		public List<ItemBasicViewModel> GetAll()
		{
		    var Items = m_Context.Items.ToList();
		    return _mapper.Map<List<ItemBasicViewModel>>(Items);
		}

        public ItemDetailViewModel Get(int id)
        {
            var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == id);
            return _mapper.Map<ItemDetailViewModel>(v_item);
        }

        public ItemBasicViewModel GetBasic(int id)
        {
            var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == id);
            return _mapper.Map<ItemBasicViewModel>(v_item);
        }

        public ServiceResult Update(ItemDetailViewModel item)
		{
		    var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == item.ID);
            if (v_item != null)
            {
	            _mapper.Map(item, v_item);
	            return SaveChanges();
            }
            return new ServiceResult(false, "");
		}

		public ServiceResult Delete(ItemDetailViewModel item)
		{
		    var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == item.ID);
            if (v_item != null)
			{
				m_Context.Items.Remove(v_item);
				return SaveChanges();
			}
            return new ServiceResult(false, "");
		}

		public ServiceResult Add(ItemDetailViewModel item)
		{
			item.IsAvaible = true;
		    var v_item = _mapper.Map<Item>(item);
            m_Context.Items.Add(v_item);
            return SaveChanges();
		}

		public List<SelectListItem> GetAvaibleSelectList()
		{
			List<SelectListItem> listItems = new List<SelectListItem>();
			listItems.Add(new SelectListItem()
			{
					Text = "Tak",
					Value = "1"
			});
			listItems.Add(new SelectListItem()
			{
				Text = "Nie",
				Value = "0"
			});

			return listItems;
		}

        public List<string> GetAvaibleItemBarcodesByBarcode(string term)
        {
            List<string> items = m_Context.Items.Where(i => i.Barcode.Contains(term) && i.Avaiable == "1").Select(i => i.Barcode).ToList();
            return items;
        }

        public ItemBasicViewModel GetAvaibleItemByBarcode(string term)
        {
            var item = _mapper.Map<ItemBasicViewModel>(m_Context.Items.Where(i => i.Barcode == term && i.Avaiable == "1").SingleOrDefault());
            return item;
        }
    }
}