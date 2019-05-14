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
using SkiRent.ViewModels.Category;

namespace SkiRent.Services
{
	public class CategoryService : BaseService<Model>
	{
	    private readonly IMapper _mapper;
	    private const string TREE_SEPARATOR = "-";

        public CategoryService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }

		public List<CategoryBasicViewModel> Filter(CategoryFilterModel model)
		{
			var items = m_Context.Categories.AsQueryable();
			if (model != null)
			{
				if (!string.IsNullOrEmpty(model.S_Name))
					items = items.Where(x => x.Name.Contains(model.S_Name));
				if (model.S_PricePerDay != 0)
					items = items.Where(x => x.PricePerDay == model.S_PricePerDay);
			}
			return _mapper.Map<List<CategoryBasicViewModel>>(items.ToList());
		}

		public List<CategoryBasicViewModel> GetAll()
		{
			var Items = m_Context.Categories.ToList();
			return _mapper.Map<List<CategoryBasicViewModel>>(Items);
		}

		public CategoryDetailViewModel Get(int id)
		{
			var v_item = m_Context.Categories.SingleOrDefault(emp => emp.ID == id);
			var tmp = _mapper.Map<CategoryDetailViewModel>(v_item);
			return tmp;
		}

		public ServiceResult Update(CategoryDetailViewModel item)
		{
			var v_item = m_Context.Categories.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				_mapper.Map(item, v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Delete(CategoryDetailViewModel item)
		{
			var v_item = m_Context.Categories.SingleOrDefault(emp => emp.ID == item.ID);
			if (v_item != null)
			{
				m_Context.Categories.Remove(v_item);
				return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Add(CategoryDetailViewModel item)
		{
			var v_item = _mapper.Map<Category>(item);
			m_Context.Categories.Add(v_item);
			return SaveChanges();
		}

        public decimal GetCategoryPricePerDay(int id)
        {
            var category = m_Context.Categories.SingleOrDefault(cat => cat.ID == id);
            var price = category.PricePerDay;
            while (price == null)
            {
                category = category.ParentCategory;
                price = category.PricePerDay;
            }

            return price.Value;
        }
		public List<SelectListItem> GetSelectCategoryList()
		{
			var categories = m_Context.Categories.ToList();
			List<SelectListItem> listItems = new List<SelectListItem>();
			foreach (Category cat in categories)
			{
				if (cat.ParentCategoryID == null)
					AddChildren(listItems,cat, 0);
			}
			return listItems;
		}

        private void AddChildren(List<SelectListItem> selectList, Category cat, int level)
        {
            var sep = "";
            for (int i = 0; i < level; i++)
                sep += TREE_SEPARATOR;
            selectList.Add(new SelectListItem
            {
                Text = sep + cat.Name,
                Value = cat.ID + ""
            });
            level++;
            if (cat.SubCategories != null)
                foreach (Category child in cat.SubCategories)
                    AddChildren(selectList, child, level);
        }
    }
}