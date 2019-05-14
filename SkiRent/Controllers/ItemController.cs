using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using SkiRent.Entities;

using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using SkiRent.ViewModels.Employee;
using X.PagedList;
using SkiRent.Extensions;
using SkiRent.ViewModels.Item;

namespace SkiRent.Controllers
{
	[Authorize]
    public class ItemController : BaseController
    {
	    private ItemService _itemService;
	    private CategoryService _categoryService;

	    public ItemController()
	    {
			var model = new Model();
			_itemService = new ItemService(model);
			_categoryService = new CategoryService(model);
	    }

	    [MvcSiteMapNode(Title = "[[[Item management]]]", ParentKey = "Home_Index", Key = "Item_Index")]
		public ActionResult Index(int? page)
        {
	        ItemIndexViewModel model = new ItemIndexViewModel();
	        model.ItemList = _itemService.GetAll().ToPagedList(page ?? 1, PAGE_SIZE);

			return View(GetIndexViewModel(model));
        }

        [HttpPost]
		public ActionResult Index(ItemFilterModel filterModel)
        {
	        ItemIndexViewModel model = new ItemIndexViewModel()
	        {
		        ItemList = filterModel.IsFiltered ? _itemService.Filter(filterModel).ToPagedList() : _itemService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel,
			};

			return View(GetIndexViewModel(model));
        }

		[MvcSiteMapNode(Title = "[[[Details]]]", ParentKey = "Item_Index", Key = "Item_Details", PreservedRouteParameters = "id")]
		public ActionResult Details(int id)
        {
            return View(_itemService.Get(id));
        }

		[MvcSiteMapNode(Title = "[[[Create]]]", ParentKey = "Item_Index", Key = "Item_Create")]
		public ActionResult Create()
		{
			ItemCreateViewModel vm = new ItemCreateViewModel()
			{
				Model = new ItemDetailViewModel()
			};
            return View(GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Create(ItemCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _itemService.Add(viewModel.Model);
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
		[MvcSiteMapNode(Title = "[[[Edit]]]", ParentKey = "Item_Index", Key = "Item_Edit", PreservedRouteParameters = "id")]
		public ActionResult Edit(int id)
		{
			ItemCreateViewModel vm  = new ItemCreateViewModel();
			vm.Model = _itemService.Get(id);

			return View("Create", GetCreateViewModel(vm));
        }

        [HttpPost]
		public ActionResult Edit(ItemCreateViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        {
		        ServiceResult result = _itemService.Update(viewModel.Model);
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

		[MvcSiteMapNode(Title = "[[[Delete]]]", ParentKey = "Item_Index", Key = "Item_Delete", PreservedRouteParameters = "id")]
		public ActionResult Delete(int id)
        {
            return View(_itemService.Get(id));
        }

        [HttpPost]
		public ActionResult Delete(ItemDetailViewModel model)
        {
	        ServiceResult result = _itemService.Delete(model);
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

		private ItemIndexViewModel GetIndexViewModel(ItemIndexViewModel currModel)
		{
			currModel.CategorySelectList = _categoryService.GetSelectCategoryList();
			currModel.AvaibleSelectList = _itemService.GetAvaibleSelectList();
			currModel.FilterModel = currModel.FilterModel ?? new ItemFilterModel();
			return currModel;
		}

		private ItemCreateViewModel GetCreateViewModel(ItemCreateViewModel currModel)
		{
			currModel.CategorySelectList = _categoryService.GetSelectCategoryList();
			return currModel;
		}
	}
}
