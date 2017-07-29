using Inven_Management.Areas.Config.Models;
using InventoryRepo.InventoryManagement;
using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
using JQueryDataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Areas.InventoryManagement.Controllers
{
    public class ProductAssaignController : Controller
    {
        #region Declare
        ProductAssaignRepo _repo = new ProductAssaignRepo();
        ProductRepo _prorepo = new ProductRepo();
        #endregion Declare
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetail(string code, string Quantity, string UnitePrice, string Remarks)
        {
            Product vm = new Product();
            ProductAssignDetail provm = new ProductAssignDetail();
            vm = _prorepo.GETAllProducts().Where(m => m.Code == code).Single();
            provm.ProductId = vm.Id;
            provm.Code = vm.Code;
            //provm.Name = vm.Name + "-" + vm.ProductSizeName +vm.UOMName;
            provm.Quantity = Convert.ToDecimal(Quantity);
            provm.UnitePrice = vm.UnitePrice;
            provm.Remarks = Remarks;
             //provm = new ProductAssignDetail() {ProductId=vm.Products.Id, Code = provm.Code, Name = provm.Name, UnitePrice = provm.UnitePrice, Quantity = provm.Quantity };
             return PartialView("_ProductAssignDetail", provm);
        }
        public ActionResult ProductDetailEdit(string code, string Quantity, string UnitePrice, string Remarks)
        {
            Product vm = new Product();
            ProductAssignDetail provm = new ProductAssignDetail();
            vm = _prorepo.GETAllProducts().Where(m => m.Code == code).Single();
            provm.ProductId = vm.Id;
            provm.Code = vm.Code;
            //provm.Name = vm.Name + "-" + vm.ProductSizeName + vm.UOMName;
            provm.Quantity = Convert.ToDecimal(Quantity);
            provm.UnitePrice = vm.UnitePrice;
            provm.Remarks = Remarks;
            //provm = new ProductAssignDetail() {ProductId=vm.Products.Id, Code = provm.Code, Name = provm.Name, UnitePrice = provm.UnitePrice, Quantity = provm.Quantity };
            return PartialView("_ProductAssignDetailEdit", provm);
        }
        public ActionResult Create()
        {
            ProductAssaignVM vm = new ProductAssaignVM();
            ProductAssign proassgn = new ProductAssign();
            
            List<ProductAssignDetail> ProductAssignDetail = new List<ProductAssignDetail>();
            vm.ProductAssignDetail = ProductAssignDetail;
            vm.proassgn = proassgn;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(ProductAssaignVM vm, string IsActive)
        {
            string[] result = new string[3];
            string mgs;
            try
            {
                //result = _repo.SaveAndEdit(vm);
                if (result[0] == "Fail")
                {
                    throw new ArgumentNullException("The expected data not found For Insert");
                }
                else
                {
                    string a = result[0] + "~" + result[1];
                    TempData["Msg"] = result[0] + "~" + result[1];
                    ViewBag.msg = result[0] + "~" + result[1];
                }
                mgs = result[0] + "~" + result[1];

            }
            catch (Exception ex)
            {
                TempData["Msg"] = result[0] + "~" + result[1] + " Error: " + ex.Message;
                mgs = result[0] + "~" + result[1];

                return Json(mgs, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Create",vm);
            }
            //return RedirectToAction("Index");
            return Json(mgs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit()
        {
            ProductAssaignVM vm = new ProductAssaignVM();
            ProductAssign proassgn = new ProductAssign();

            List<ProductAssignDetail> ProductAssignDetail = new List<ProductAssignDetail>();
            vm.ProductAssignDetail = ProductAssignDetail;
            
            vm.proassgn = proassgn;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Edit(ProductAssaignVM vm, string IsActive)
        {
            string[] result = new string[3];
            string mgs;
            try
            {
                //result = _repo.SaveAndEdit(vm);
                if (result[0] == "Fail")
                {
                    throw new ArgumentNullException("The expected data not found For Insert");
                }
                else
                {
                    string a = result[0] + "~" + result[1];
                    TempData["Msg"] = result[0] + "~" + result[1];
                    ViewBag.msg = result[0] + "~" + result[1];
                }
                mgs = result[0] + "~" + result[1];

            }
            catch (Exception ex)
            {
                TempData["Msg"] = result[0] + "~" + result[1] + " Error: " + ex.Message;
                mgs = result[0] + "~" + result[1];

                return Json(mgs, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Create",vm);
            }
            //return RedirectToAction("Index");
            return Json(mgs, JsonRequestBehavior.AllowGet);
        }
    }
}
