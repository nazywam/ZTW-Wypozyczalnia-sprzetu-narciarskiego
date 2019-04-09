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
using SkiRent.Entities.DTO;
using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;

namespace SkiRent.Services
{
	public class ItemService : BaseService<Model>, IService<ItemDTO>
	{
	    private readonly IMapper _mapper;

        public ItemService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }
			
		public List<ItemDTO> Filter(ItemFilterModel model)
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
			return _mapper.Map<List<ItemDTO>>(items.ToList());
		}

		public List<ItemDTO> GetAll()
		{
		    var Items = m_Context.Items.ToList();
		    return _mapper.Map<List<ItemDTO>>(Items);
		}

        public ItemDTO Get(int id)
        {
            var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == id);
            return _mapper.Map<ItemDTO>(v_item);
        }

		public ServiceResult Update(ItemDTO item)
		{
		    var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == item.ID);
            if (v_item != null)
            {
	            _mapper.Map(item, v_item);
	            return SaveChanges();
            }
            return new ServiceResult(false, "");
		}

		public ServiceResult Delete(ItemDTO item)
		{
		    var v_item = m_Context.Items.SingleOrDefault(emp => emp.ID == item.ID);
            if (v_item != null)
			{
				m_Context.Items.Remove(v_item);
				return SaveChanges();
			}
            return new ServiceResult(false, "");
		}

		public ServiceResult Add(ItemDTO item)
		{
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
	}
}