using InventoryRepo.InventoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Areas.Config.Controllers
{
    public class AutoCompleteController : Controller
    {
        //
        // GET: /Config/AutoComplete/

        public ActionResult Index()
        {
            return View();
        }
        //public JsonResult Product(string term)
        //{
        //return Json(new SelectList(new ProductRepo().Autocomplete(term), "Id", "Name"), JsonRequestBehavior.AllowGet);
        //}
        public JsonResult Product(string term)
        {
            return Json(new ProductRepo().Autocomplete(term), JsonRequestBehavior.AllowGet);
        }
    }
}
