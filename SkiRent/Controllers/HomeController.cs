using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSiteMapProvider;
using SkiRent.Authorization;
using SkiRent.Entities;

using SkiRent.Extensions;
using SkiRent.Services;
using SkiRent.ViewModels;


namespace SkiRent.Controllers
{
	public class HomeController : BaseController
    {
        private ItemService _itemService;

        public HomeController()
        {
            var model = new Model();
            _itemService = new ItemService(model);
        }

        [MvcSiteMapNode(Title = "[[[Home]]]", Key = "Home_Index")]
		public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel filterModel)
        {
            var ItemId = _itemService.GetAll().Where(i => i.Barcode == filterModel.S_Barcode).SingleOrDefault();
            if (ItemId != null)
                return RedirectToAction("Details", "Item", new {id = ItemId.ID});
            else
                return View();
        }
    }
}