using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SkiRent.Entities;
using SkiRent.Entities.DTO;
using SkiRent.Extensions;

namespace SkiRent.Services
{
	public class CategoryService : BaseService<Model>, IService<CategoryDTO>
	{
	    private readonly IMapper _mapper;

        public CategoryService(Model context) : base(context)
		{
		    _mapper = MapperService.GetMapperInstance();
        }

		public List<CategoryDTO> GetAll()
		{
            var categories = m_Context.Categories.ToList();
		    return _mapper.Map<List<CategoryDTO>>(categories);
		}

		public CategoryDTO Get(int id)
		{
            var category = m_Context.Categories.SingleOrDefault(item => item.ID == id);
		    return _mapper.Map<CategoryDTO>(category);
		}

		public ServiceResult Update(CategoryDTO item)
		{
		    var category = m_Context.Categories.SingleOrDefault(cat => cat.ID == item.ID);
			if (category != null)
			{
			    _mapper.Map(item, category);
			    return SaveChanges();
			}
			return new ServiceResult(false, "");
		}

		public ServiceResult Delete(CategoryDTO item)
		{
			var category = m_Context.Categories.SingleOrDefault(cat => cat.ID == item.ID);
		    if (category != null)
		    {
		        m_Context.Categories.Remove(category);
				return SaveChanges();
			}
		    return new ServiceResult(false, "");
		}

		public ServiceResult Add(CategoryDTO item)
		{
		    var category = _mapper.Map<Category>(item);
		    m_Context.Categories.Add(category);
		    return SaveChanges();
		}

		public List<SelectListItem> GetSelectCategoryList()
		{
			var categories = GetAll();
			List<SelectListItem> listItems = new List<SelectListItem>();
			foreach (CategoryDTO cat in categories)
			{
				listItems.Add(new SelectListItem()
				{
					Text = cat.Name,
					Value = cat.ID.ToString()
				});
			}

			return listItems;
		}
	}
}