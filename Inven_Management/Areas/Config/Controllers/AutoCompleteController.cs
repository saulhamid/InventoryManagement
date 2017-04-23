using InventoryRepo.Config;
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
        public JsonResult ProductWithCodeName(string term)
        {
            return Json(new ProductRepo().AutocompleteWithCodeName(term), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ZoneOrArea(string term)
        {
            return Json(new ZoneorAreaRepo().Autocomplete(term), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Market(string term)
        {
            return Json(new MarketRepo().Autocomplete(term), JsonRequestBehavior.AllowGet);
        }
        public JsonResult StockProduct(string term)
        {
            return Json(new StockRepo().Autocomplete(term), JsonRequestBehavior.AllowGet);
        }
        public JsonResult StockProductwithUnitePrice(string term)
        {
            return Json(new StockRepo().AutocompleteUnitePrice(term), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutocompleteInvoice(string term)
        {
            return Json(new StockRepo().AutocompleteInvoice(term), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutocompleteInvoicePurchease(string term)
        {
            return Json(new PurcheaseRepo().AutocompleteInvoice(term), JsonRequestBehavior.AllowGet);
        }
    }
}
