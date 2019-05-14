using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using X.PagedList;
using SkiRent.Entities;
using SkiRent.Extensions;
using SkiRent.ViewModels.Category;
using SkiRent.ViewModels.Item;

namespace SkiRent.Controllers
{
	[Authorize]
    public class CategoryController : BaseController
    {
	    private CategoryService _categoryService;

	    public CategoryController()
	    {
			var model = new Model();
			_categoryService = new CategoryService(model);
	    }

	    [MvcSiteMapNode(Title = "[[[Category management]]]", ParentKey = "Home_Index", Key = "Category_Index")]
		public ActionResult Index(int? page)
        {
	        CategoryIndexViewModel model = new CategoryIndexViewModel();
	        model.CategoryList = _categoryService.GetAll().ToPagedList(page ?? 1, PAGE_SIZE);

			return View(GetIndexViewModel(model));
        }

        [HttpPost]
		public ActionResult Index(CategoryFilterModel filterModel)
        {
	        CategoryIndexViewModel model = new CategoryIndexViewModel()
	        {
		        CategoryList = filterModel.IsFiltered ? _categoryService.Filter(filterModel).ToPagedList() : _categoryService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel,
			};

			return View(GetIndexViewModel(model));
        }

		[MvcSiteMapNode(Title = "[[[Details]]]", ParentKey = "Category_Index", Key = "Category_Details", PreservedRouteParameters = "id")]
		public ActionResult Details(int id)
        {
            return View(_categoryService.Get(id));
        }

		[MvcSiteMapNode(Title = "[[[Create]]]", ParentKey = "Category_Index", Key = "Category_Create")]
		public ActionResult Create()
		{
			CategoryCreateViewModel vm = new CategoryCreateViewModel()
			{
				Model = new CategoryDetailViewModel()
			};
            return View(GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Create(CategoryCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _categoryService.Add(viewModel.Model);
				if (result.Status)
		        {
			        AddSuccessCreatedToastMessage();
			        return RedirectToAction("Index");
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
			        return View(GetCreateViewModel(viewModel));
		        }
	        }
	        return View(GetCreateViewModel(viewModel));
		}

		[MvcSiteMapNode(Title = "[[[Edit]]]", ParentKey = "Category_Index", Key = "Category_Edit", PreservedRouteParameters = "id")]
		public ActionResult Edit(int id)
		{
			CategoryCreateViewModel vm  = new CategoryCreateViewModel();
			vm.Model = _categoryService.Get(id);

			return View("Create", GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Edit(CategoryCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _categoryService.Update(viewModel.Model);
		        if (result.Status)
		        {
			        AddSuccessEditedToastMessage();
			        return RedirectToAction("Index");
		        }
		        else
		        {
			        AddServiceErrorToastMessage(result);
					return View("Create", GetCreateViewModel(viewModel));
				}
	        }
			return View("Create", GetCreateViewModel(viewModel));
		}

		[MvcSiteMapNode(Title = "[[[Delete]]]", ParentKey = "Category_Index", Key = "Category_Delete", PreservedRouteParameters = "id")]
		public ActionResult Delete(int id)
        {
            return View(_categoryService.Get(id));
        }

        [HttpPost]
		public ActionResult Delete(CategoryDetailViewModel model)
        {
	        ServiceResult result = _categoryService.Delete(model);
	        if (result.Status)
	        {
		        AddSuccessDeletedToastMessage();
		        return RedirectToAction("Index");
	        }
	        else
	        {
		        AddServiceErrorToastMessage(result);
		        return RedirectToAction("Delete", new { model.ID });
	        }
		}

		private CategoryIndexViewModel GetIndexViewModel(CategoryIndexViewModel currModel)
		{
			currModel.FilterModel = currModel.FilterModel ?? new CategoryFilterModel();
			return currModel;
		}

		private CategoryCreateViewModel GetCreateViewModel(CategoryCreateViewModel currModel)
		{
			currModel.CategorySelectList = _categoryService.GetSelectCategoryList();
			return currModel;
		}
	}
}
