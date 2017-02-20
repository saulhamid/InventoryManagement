using InventoryRepo.InventoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Areas.Config.Controllers
{
    public class DropDownController : Controller
    {
        #region Declare

        #endregion Declare
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Product()
        {
            return Json(new SelectList(new ProductRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductwithCode()
        {
            return Json(new SelectList(new ProductRepo().DropdownWithCode(), "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductBrand()
        {
            return Json(new SelectList(new ProductBrandRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductCategory()
        {
            var a=new SelectList(new ProductCategoryRepo().Dropdown(), "Id", "Name");
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductColor()
        {
            return Json(new SelectList(new ProductColorRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductSize()
        {
            return Json(new SelectList(new ProductSizeRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductType()
        {
            return Json(new SelectList(new ProductTypeRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CustomerRepo()
        {
            return Json(new SelectList(new CustomerRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Employee()
        {
            return Json(new SelectList(new EmployeeRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Supplier()
        {
            return Json(new SelectList(new SupplierRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UOM()
        {
            return Json(new SelectList(new UOMRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ZoneorArea()
        {
            return Json(new SelectList(new ZoneorAreaRepo().Dropdown(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
    }
}
