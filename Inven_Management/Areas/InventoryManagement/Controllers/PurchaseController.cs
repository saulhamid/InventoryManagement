using Inven_Management.Areas.Config.Models;
using Inven_Management.Areas.InventoryManagement.Models;
using InventoryRepo.InventoryManagement;
using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
using JQueryDataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Areas.InventoryManagement.Controllers
{
    public class PurchaseController : Controller
    {
        #region Declare
        PurcheaseRepo _repo = new PurcheaseRepo();
        InventoryEntities _context = new InventoryEntities();

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
            var getAllData = _repo.GETAllPurchases();
            IEnumerable<PurchaseVM> filteredData;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);
                filteredData = getAllData.Where(c => isSearchable1 && c.InvoiecNo.ToLower().Contains(param.sSearch.ToLower())
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
                                            && (DateFilter == "" || c.Date.ToLower().Contains(DateFilter.ToLower()))
                                            && (SupplierFilter == "" || c.SupplierName.ToString().ToLower().Contains(SupplierFilter.ToLower()))
                                            && (TotalPriceFilter == "" || c.UnitPrice.ToString().ToLower().Contains(TotalPriceFilter.ToLower())));
            }
            #endregion Column Filtering
            var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var isSortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PurchaseVM, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.InvoiecNo :
                                                           sortColumnIndex == 2 && isSortable_2 ? c.Date :
                                                           sortColumnIndex == 3 && isSortable_2 ? c.SupplierName.ToString() :
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
                ,c.Date
                ,c.SupplierName
                ,c.EmployeeName
                ,c.TotalDiscount.ToString()
                ,c.GrandTotal.ToString()
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
        public ActionResult ProductDetail(string code, string Quantity, string UnitePrice, string Discount,string Remarks)
        {
            Product vm = new Product();
            PurcheaseDetail purvmd = new PurcheaseDetail();
            purvmd.IsActive = true;
            vm = new ProductRepo().GETAllByCode(code);
            purvmd.Code = vm.Code;
            purvmd.ProductId = vm.Id;
            purvmd.Name = vm.Name + "-" + vm.ProductSizeName + vm.UOMName;
            purvmd.Quantity = Convert.ToDecimal(Quantity);
            purvmd.UnitePrice = Convert.ToDecimal(UnitePrice);
            purvmd.Discount = Convert.ToDecimal(Discount);
            purvmd.Remarks = Remarks;
            return PartialView("_purcheaseDetail", purvmd);
        }
        public ActionResult Create()
        {
            PurcheaseDetailVM vm = new PurcheaseDetailVM();
            Purchase Purchase = new Purchase();
            List<PurcheaseDetail> PurcheaseDetails = new List<PurcheaseDetail>();
            vm.Purchasevm = Purchase;
            vm.PurcheaseDetails = PurcheaseDetails;
            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(PurcheaseDetailVM vm, string IsActive)
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
                }
                 mgs = result[0] + "~" + result[1];

            }
            catch (Exception ex)
            {
                mgs = result[0] + "~" + result[1];
                return Json(mgs, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Create",vm);
            }
            //return RedirectToAction("Index");
            return Json(mgs, JsonRequestBehavior.AllowGet);
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
        public JsonResult Delete(string ids)
        {
            Purchase data = new Purchase();
            string[] a = ids.Split('~');
            string[] result = new string[6];
            result = _repo.Delete(a);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
