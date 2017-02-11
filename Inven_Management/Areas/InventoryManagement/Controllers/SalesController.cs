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
    public class SalesController : Controller
    { 
        #region Declare
        SalesRepo _repo = new SalesRepo();
        ProductRepo _prorepo = new ProductRepo();
        #endregion Declare
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _index(JQueryDataTableParamModel param)//EmployeeId
        {
            #region Column Search
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var InvoiceFilter = Convert.ToString(Request["sSearch_1"]);
            var DateFilter = Convert.ToString(Request["sSearch_2"]);
            var SupplierFilter = Convert.ToString(Request["sSearch_3"]);
            var TotalPriceFilter = Convert.ToString(Request["sSearch_4"]);
            #endregion Column Search

            var getAllData = _repo.GETAllSales();
            IEnumerable<SalesVM> filteredData;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);
                filteredData = getAllData.Where(c => isSearchable1 && c.InvoiecNo.ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable2 && c.ProductName.ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable3 && c.ProductName.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable3 && c.ProductName.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable4 && c.TotalDiscount.ToString().ToLower().Contains(param.sSearch.ToLower())
                               );
            }
            else
            {
                filteredData = getAllData;
            }
            #region Column Filtering
            if (InvoiceFilter != "" || DateFilter != "" || SupplierFilter != "")
            {
                filteredData = getAllData.Where(c => (InvoiceFilter == "" || c.InvoiecNo.ToLower().Contains(InvoiceFilter.ToLower()))
                                            && (DateFilter == "" || c.ProductName.ToLower().Contains(DateFilter.ToLower()))
                                            && (SupplierFilter == "" || c.ProductName.ToString().ToLower().Contains(SupplierFilter.ToLower()))
                                            && (TotalPriceFilter == "" || c.ProductName.ToString().ToLower().Contains(TotalPriceFilter.ToLower())));
            }
            #endregion Column Filtering
            var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var isSortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<SalesVM, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.InvoiecNo :
                                                           sortColumnIndex == 2 && isSortable_2 ? c.ProductName :
                                                           sortColumnIndex == 3 && isSortable_2 ? c.ProductName.ToString() :
                                                           sortColumnIndex == 4 && isSortable_3 ? c.TotalPrice.ToString() : "");
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredData = filteredData.OrderBy(orderingFunction);
            else
                filteredData = filteredData.OrderByDescending(orderingFunction);
            var displayedCompanies = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { 
                 Convert.ToString(c.Id)
                ,c.InvoiecNo
                ,c.ProductName
                ,c.UnitePrice.ToString()
                ,c.ReceiveQuantity.ToString()
                ,c.SalesQuantity.ToString()
                ,c.Return.ToString()
                ,c.Replace.ToString()
                ,c.Slup.ToString()
                ,c.Discount.ToString()
                ,c.Datetime
                ,c.WithOurDiscountPrice.ToString()
                ,c.CustomerName
                ,c.ZoneOrAreaNae
                         };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = getAllData.Count()
                ,
                iTotalDisplayRecords = filteredData.Count()
                ,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            SalesVM vm= new SalesVM();
            Sale salevm = new Sale();
            List<SalesDetail> SalesDetailvms = new List<SalesDetail>();
            vm.Sales = salevm;
            vm.SalesDetailvms = SalesDetailvms;
            return View(vm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(SalesVM vm)
        {
            string[] result = new string[3];
            string mgs;
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
        public ActionResult ProductDetail(string code, decimal UnitePrice = 0, decimal reivecqty = 0, decimal returns = 0, decimal replace = 0, decimal slup = 0, decimal Discount = 0, string Remarks = "")
        {
            Product vm = new Product();
            SalesDetail provm = new SalesDetail();
            vm = _prorepo.GETAllByCode(code);
            provm.ProductId = vm.Id;
            provm.Code = vm.Code;
            provm.Name =vm.Code+"-"+ vm.Name + "-" + vm.ProductSizeName + vm.UOMName;
            provm.UnitePrice = Convert.ToDecimal(UnitePrice);
            provm.ReceiveQuantity = Convert.ToDecimal(reivecqty);
            provm.Slup = Convert.ToDecimal(slup);
            provm.Return = Convert.ToDecimal(returns);
            provm.Replace = Convert.ToDecimal(replace);
            provm.Discount = Convert.ToDecimal(Discount);
            provm.SalesQuantity = provm.ReceiveQuantity-provm.Slup-provm.Replace-provm.Return;
            provm.Remarks = Remarks;
            provm.TotalSlupPrice = provm.UnitePrice * provm.Slup;
            provm.WithOurDiscountPrice = provm.UnitePrice * provm.ReceiveQuantity;
            provm.TotalAmount = provm.WithOurDiscountPrice-provm.TotalSlupPrice-(provm.Replace*provm.UnitePrice)-(provm.Replace*provm.UnitePrice)-provm.Discount;
            provm.Remarks = Remarks;
            return PartialView("_SalesDetail", provm);
        }
        public ActionResult Edit(int Id)
        {
            SalesVM vm = new SalesVM();
            vm = _repo.GetSigle(Id);
            return View("Create", vm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(SalesVM vm)
        {
            string[] result = new string[3];
            string mgs;
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
