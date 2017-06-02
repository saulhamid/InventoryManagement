using Inven_Management.Areas.Config.Models;
using Inven_Management.Areas.InventoryManagement.Models;
using InventoryRepo.InventoryManagement;
using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
using JQueryDataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Controllers
{
    public class PurchaseController : Controller
    {
        #region Declare
        PurcheaseRepo _repo = new PurcheaseRepo();
        #endregion Declare
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult _index(JQueryDataTableParamModel param)//EmployeeId
        //{
        //    #region Column Search
        //    var idFilter = Convert.ToString(Request["sSearch_0"]);
        //    var codeFilter = Convert.ToString(Request["sSearch_1"]);
        //    var nameFilter = Convert.ToString(Request["sSearch_2"]);
        //    var isActiveFilter = Convert.ToString(Request["sSearch_3"]);
        //    var RemarkFilter = Convert.ToString(Request["sSearch_4"]);
        //    var isActiveFilter1 = isActiveFilter.ToLower() == "y" ? true.ToString() : false.ToString();
        //    #endregion Column Search

        //    var getAllData = _repo.GETAllPurchases();
        //    IEnumerable<PurcheaseDetailVM> filteredData;
        //    //Check whether the companies should be filtered by keyword
        //    if (!string.IsNullOrEmpty(param.sSearch))
        //    {
        //        //Optionally check whether the columns are searchable at all 
        //        var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
        //        var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
        //        var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
        //        filteredData = getAllData.Where(c => isSearchable1 && c.Productvm.Name.ToLower().Contains(param.sSearch.ToLower())
        //                       || isSearchable2 && c.Productvm.Code.ToLower().Contains(param.sSearch.ToLower())
        //                       || isSearchable3 && c.Productvm.Remarks.ToLower().Contains(param.sSearch.ToLower()));
        //    }
        //    else
        //    {
        //        filteredData = getAllData;
        //    }
        //    #region Column Filtering
        //    if (codeFilter != "" || nameFilter != "" || isActiveFilter != "")
        //    {
        //        filteredData = getAllData.Where(c => (codeFilter == "" || c.Productvm.Code.ToLower().Contains(codeFilter.ToLower()))
        //                                    && (nameFilter == "" || c.Productvm.Name.ToLower().Contains(nameFilter.ToLower()))
        //                                    && (isActiveFilter == "" || c.Productvm.IsActive.ToString().ToLower().Contains(isActiveFilter1.ToLower()))
        //                                    && (RemarkFilter == "" || c.Productvm.Remarks.ToLower().Contains(RemarkFilter.ToLower())));
        //    }
        //    #endregion Column Filtering
        //    var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
        //    var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
        //    var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
        //    var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
        //    Func<PurcheaseDetailVM, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.Productvm.Name :
        //                                                   sortColumnIndex == 2 && isSortable_2 ? c.Productvm.Code :
        //                                                   sortColumnIndex == 3 && isSortable_3 ? c.Productvm.Remarks : "");
        //    var sortDirection = Request["sSortDir_0"]; // asc or desc
        //    if (sortDirection == "asc")
        //        filteredData = filteredData.OrderBy(orderingFunction);
        //    else
        //        filteredData = filteredData.OrderByDescending(orderingFunction);
        //    var displayedCompanies = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength);
        //    var result = from c in displayedCompanies
        //                 select new[] { 
        //         Convert.ToString(c.Productvm.Id)
        //        ,c.Productvm.Code
        //        ,c.Productvm.Name
        //        ,c.PurcheaseDetail.Quantity.ToString()
        //        ,c.PurcheaseDetail.UnitePrice.ToString()
        //        ,c.PurcheaseDetail.TotalPrice.ToString()
        //        ,c.Productvm.Remarks };
        //    return Json(new
        //    {
        //        sEcho = param.sEcho,
        //        iTotalRecords = getAllData.Count()
        //        ,
        //        iTotalDisplayRecords = filteredData.Count()
        //        ,
        //        aaData = result
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ProductDetail(string code, string Quantity, string UnitePrice, string Discount,string Remarks)
        {
            Product vm = new Product();
            ProductDateilVM provm = new ProductDateilVM();
            vm.IsActive = true;
            vm = new ProductRepo().GETAllProducts().FirstOrDefault(m => m.Code == code);
            vm = provm.Products;
            vm.Id = provm.Products.Id;
            vm.Name = provm.Products.Name + "-" + provm.UOMs.Name;
            vm.Quantity =Convert.ToDecimal(Quantity);
            vm.UnitePrice = Convert.ToDecimal(UnitePrice);
            vm.Discount = Convert.ToDecimal(Discount);
            vm.Remarks = Remarks;
            var products=new Product() {Code=vm.Code,Name=vm.Name,UnitePrice=vm.UnitePrice,Quantity=vm.Quantity };
            Session["td"] = products;
            return PartialView("_purcheaseDetail", vm);
        }
        public ActionResult Create()
        {
            PurcheaseDetailVM vm = new PurcheaseDetailVM();
            List<Product> Productvms = new List<Product>();
            Product Productvm = new Product();
            Purchase Purchasevm = new Purchase();
            vm.Productvms = Productvms;
            vm.Purchasevm = Purchasevm;
            vm.Productvm = Productvm;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(PurcheaseDetailVM vm, string IsActive)
        {
            string[] result = new string[3];
            try
            {
                result = _repo.SaveAndEdit(vm);
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
            }
            catch (Exception ex)
            {
                TempData["Msg"] = result[0] + "~" + result[1] + " Error: " + ex.Message;
                ViewBag.msg = result[0] + "~" + result[1];
                return RedirectToAction("Create",vm);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            PurcheaseDetailVM vm = new PurcheaseDetailVM();

            vm = _repo.GetSigle(Id);
            return View("Create", vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurcheaseDetailVM vm)
        {
            string[] result = new string[3];
            try
            {
                result = _repo.SaveAndEdit(vm);
                if (result[0] == "Fail")
                {
                    throw new ArgumentNullException("The expected data not found For Insert");
                }
                TempData["Msg"] = result[0] + "~" + result[1];
            }
            catch (Exception ex)
            {
                ViewBag.fail = result[0] + " " + result[1] + " Error: " + ex.Message;
                return RedirectToAction("Create",vm);
            }
            return RedirectToAction("Index");
        }

    }
}
