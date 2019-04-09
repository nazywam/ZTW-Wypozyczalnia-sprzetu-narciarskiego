using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using SkiRent.Entities;
using SkiRent.Entities.DTO;

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

		public void Update(CategoryDTO item)
		{

		}

		public void Delete(CategoryDTO item)
		{

        }

		public void Add(CategoryDTO item)
		{

		}
	}
}