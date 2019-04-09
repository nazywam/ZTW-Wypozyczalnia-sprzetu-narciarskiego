using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkiRent.Entities;
using SkiRent.Entities.DTO;
using SkiRent.Entities.FilterModels;
using SkiRent.Services;
using SkiRent.ViewModels.Employee;
using X.PagedList;
using MvcBreadCrumbs;

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

		[BreadCrumb(Clear = true, Label = "Zarządzanie sprzętem")]
        public ActionResult Index(int? page)
        {
            ItemIndexViewModel model = new ItemIndexViewModel()
            {
                ItemList = _itemService.GetAll().ToPagedList(page ?? 1, PAGE_SIZE),
                FilterModel = new ItemFilterModel(),
				CategorySelectList = _categoryService.GetSelectCategoryList(),
				AvaibleSelectList = _itemService.GetAvaibleSelectList()
            };

            return View(model);
        }

        [HttpPost]
		public ActionResult Index(ItemFilterModel filterModel)
        {
	        ItemIndexViewModel model = new ItemIndexViewModel()
	        {
		        ItemList = filterModel.IsFiltered ? _itemService.Filter(filterModel).ToPagedList() : _itemService.GetAll().ToPagedList(1, PAGE_SIZE),
				FilterModel = filterModel,
				CategorySelectList = _categoryService.GetSelectCategoryList(),
				AvaibleSelectList = _itemService.GetAvaibleSelectList()
			};

			return View(model);
        }

		[BreadCrumb(Label = "Szczegóły")]
		public ActionResult Details(int id)
        {
            return View(_itemService.Get(id));
        }

		[BreadCrumb(Label = "Dodaj")]
		public ActionResult Create()
        {
            return View(new EmployeeDTO());
        }

        [HttpPost]
		public ActionResult Create(ItemDTO model)
        {
            if (ModelState.IsValid)
            {
	            _itemService.Add(model);
	            AddSuccessCreatedToastMessage();
	            return RedirectToAction("Index");
			}
	        return View(model);
        }

		[BreadCrumb(Label = "Edytuj")]
		public ActionResult Edit(int id)
        {
            return View("Create", _itemService.Get(id));
        }

        [HttpPost]
		public ActionResult Edit(ItemDTO model)
        {
	        if (ModelState.IsValid)
	        {
		        AddSuccessEditedToastMessage();
		        _itemService.Update(model);
		        return RedirectToAction("Index");
	        }
		    return View("Create",model);
		}

		[BreadCrumb(Label = "Usuń")]
		public ActionResult Delete(int id)
        {
            return View(_itemService.Get(id));
        }

        [HttpPost]
		public ActionResult Delete(ItemDTO model)
        {
	        AddSuccessDeletedToastMessage();
	        _itemService.Delete(model);
	        return RedirectToAction("Index");
		}
    }
}
