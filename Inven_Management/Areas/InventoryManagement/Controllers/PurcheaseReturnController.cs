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
    public class PurcheaseReturnController : Controller
    {
        PurcheaseReturnRepo _repo = new PurcheaseReturnRepo();
        ProductRepo _repoProduct = new ProductRepo();
        PurcheaseRepo _repoPurchease = new PurcheaseRepo();
        StockRepo _repostock = new StockRepo();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Indexdetail(string invoceno)
        {
            if (invoceno != null)
                ViewBag.invoceno = invoceno;
            return PartialView("Indexdetail");
        }
        public ActionResult ProductDetail(int PurcheasId, int productId, string Quantity, string Discount, string Remarks)
        {
            PurcheaseReturnDetail vm = new PurcheaseReturnDetail();
            var pur = _repoPurchease.GetSigle(PurcheasId).PurcheaseDetails.Where(m => m.ProductId == productId).FirstOrDefault();
            vm.ProductId = pur.ProductId;
            vm.Quantity = Convert.ToDecimal(Quantity);
            vm.Discount = Convert.ToDecimal(Discount);
            vm.Remarks = Remarks;
            return PartialView("_purcheaseDetail", vm);
        }
         public ActionResult ProductDetail1(int Id)
        {
            PurcheaseReturnDetail vm = new PurcheaseReturnDetail();
            var stock = _repostock.GetSigle(Id);
            vm.ProductId = stock.ProductId;
            vm.Quantity = 0;
            vm.Discount = 0;
            return PartialView("_purcheaseDetail", vm);
        }
        public ActionResult _indexs(JQueryDataTableParamModel param)
        {
            #region Column Search
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var InvoiceFilter = Convert.ToString(Request["sSearch_1"]);
            var DateFilter = Convert.ToString(Request["sSearch_2"]);
            var SupplierFilter = Convert.ToString(Request["sSearch_3"]);
            var TotalPriceFilter = Convert.ToString(Request["sSearch_4"]);
            var UnitPriceFilter = Convert.ToString(Request["sSearch_5"]);
            #endregion Column Search
            var getAllData = _repo.GETAllPurcheaseReturns();
            IEnumerable<PurcheaseReturnVM> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);
                var isSearchable5 = Convert.ToBoolean(Request["bSearchable_5"]);
                var isSearchable6 = Convert.ToBoolean(Request["bSearchable_6"]);
                filteredData = getAllData.Where(c => isSearchable1 && c.InvoiceNo.ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable3 && c.Date.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable3 && c.ProductName.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable4 && c.Quantity.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable5 && c.Quantity.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable6 && c.Quantity.ToString().ToLower().Contains(param.sSearch.ToLower())
                               );
            }
            else
            {
                filteredData = getAllData;
            }
            #region Column Filtering
            if (InvoiceFilter != "" || DateFilter != "" || SupplierFilter != "")
            {
                filteredData = getAllData.Where(c => (InvoiceFilter == "" || c.InvoiceNo.ToLower().Contains(InvoiceFilter.ToLower()))
                                            && (DateFilter == "" || c.Date.ToLower().Contains(DateFilter.ToLower()))
                                            && (SupplierFilter == "" || c.ProductName.ToString().ToLower().Contains(SupplierFilter.ToLower()))
                                            && (TotalPriceFilter == "" || c.Quantity.ToString().ToLower().Contains(TotalPriceFilter.ToLower())));
            }
            #endregion Column Filtering
            var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var isSortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var isSortable_5 = Convert.ToBoolean(Request["bSortable_4"]);
            var isSortable_6 = Convert.ToBoolean(Request["bSortable_4"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PurcheaseReturnVM, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.InvoiceNo :
                                                           sortColumnIndex == 2 && isSortable_2 ? c.Date :
                                                           sortColumnIndex == 3 && isSortable_3 ? c.ProductName.ToString() :
                                                           sortColumnIndex == 4 && isSortable_4 ? c.ProductName.ToString() :
                                                           sortColumnIndex == 5 && isSortable_5 ? c.ProductName.ToString() :
                                                           sortColumnIndex == 7 && isSortable_6 ? c.ProductName.ToString() :
                                                           sortColumnIndex == 4 && isSortable_3 ? c.Quantity.ToString() : "");
            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredData = filteredData.OrderBy(orderingFunction);
            else
                filteredData = filteredData.OrderByDescending(orderingFunction);
            var displayedCompanies = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { 
                 c.Id.ToString()
                ,c.InvoiceNo
                ,c.Date
                ,c.ProductName
                ,c.Quantity.ToString()
                ,c.UnitePrice.ToString()
                ,c.Discount.ToString()
                ,c.SupplierName
                         };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = getAllData.Count(),
                iTotalDisplayRecords = filteredData.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _index(JQueryDataTableParamModel param, string invoceno=null)
        {
            #region Column Search
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var InvoiceFilter = Convert.ToString(Request["sSearch_1"]);
            var DateFilter = Convert.ToString(Request["sSearch_2"]);
            var SupplierFilter = Convert.ToString(Request["sSearch_3"]);
            var TotalPriceFilter = Convert.ToString(Request["sSearch_4"]);
            var UnitPriceFilter = Convert.ToString(Request["sSearch_5"]);
            #endregion Column Search
            var getAllData = _repo.GETAllPurchasesByInvoice(invoceno);
            IEnumerable<PurchaseVM> filteredData;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);
                var isSearchable5 = Convert.ToBoolean(Request["bSearchable_5"]);
                var isSearchable6 = Convert.ToBoolean(Request["bSearchable_6"]);
                filteredData = getAllData.Where(c => isSearchable1 && c.InvoiecNo.ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable3 && c.Date.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable3 && c.ProductName.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable4 && c.Quantity.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable5 && c.Quantity.ToString().ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable6 && c.Quantity.ToString().ToLower().Contains(param.sSearch.ToLower())
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
                                            && (SupplierFilter == "" || c.ProductName.ToString().ToLower().Contains(SupplierFilter.ToLower()))
                                            && (TotalPriceFilter == "" || c.Quantity.ToString().ToLower().Contains(TotalPriceFilter.ToLower())));
            }
            #endregion Column Filtering
                var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
                var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
                var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
                var isSortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
                var isSortable_5 = Convert.ToBoolean(Request["bSortable_4"]);
                var isSortable_6 = Convert.ToBoolean(Request["bSortable_4"]);
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Func<PurchaseVM, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.InvoiecNo :
                                                               sortColumnIndex == 2 && isSortable_2 ? c.Date :
                                                               sortColumnIndex == 3 && isSortable_3 ? c.ProductName.ToString() :
                                                               sortColumnIndex == 4 && isSortable_4 ? c.ProductName.ToString() :
                                                               sortColumnIndex == 5 && isSortable_5 ? c.ProductName.ToString() :
                                                               sortColumnIndex == 7 && isSortable_6 ? c.ProductName.ToString() :
                                                               sortColumnIndex == 4 && isSortable_3 ? c.Quantity.ToString() : "");
                var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredData = filteredData.OrderBy(orderingFunction);
            else
                filteredData = filteredData.OrderByDescending(orderingFunction);
            var displayedCompanies = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { 
                 c.Id.ToString()
                ,c.InvoiecNo
                ,c.Date
                ,c.ProductName +"-"+c.ProductCode
                ,c.Quantity.ToString()
                ,c.UnitPrice.ToString()
                ,c.Discount.ToString()
                ,c.SupplierName
                ,c.EmployeeName };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = getAllData.Count() ,
                iTotalDisplayRecords = filteredData.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _indexStock(JQueryDataTableParamModel param)//EmployeeId
        {
            #region Column Search
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var InvoiceFilter = Convert.ToString(Request["sSearch_1"]);
            var DateFilter = Convert.ToString(Request["sSearch_2"]);
            var SupplierFilter = Convert.ToString(Request["sSearch_3"]);
            var TotalPriceFilter = Convert.ToString(Request["sSearch_4"]);
            #endregion Column Search

            var getAllData = _repostock.GETAllStockstor();
            IEnumerable<StockVM> filteredData;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);
                filteredData = getAllData.Where(c => isSearchable1 && c.ProductCode.ToLower().Contains(param.sSearch.ToLower())
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
                filteredData = getAllData.Where(c => (InvoiceFilter == "" || c.ProductCode.ToLower().Contains(InvoiceFilter.ToLower()))
                                            && (SupplierFilter == "" || c.SupplierName.ToString().ToLower().Contains(SupplierFilter.ToLower()))
                                            && (TotalPriceFilter == "" || c.UnitPrice.ToString().ToLower().Contains(TotalPriceFilter.ToLower())));
            }
            #endregion Column Filtering
            var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var isSortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<StockVM, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.SupplierName :
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
                ,c.ProductCode+"-"+c.ProductName
                ,c.TotalQuantity.ToString()
                ,c.UnitPrice.ToString()
                ,(c.TotalPrice=c.TotalQuantity+c.UnitPrice).ToString()
                //,c.TotalReplace.ToString()
                //,c.TotalReturn.ToString()
                //,c.TotalSlup.ToString()
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
        public ActionResult Create(string PId)
        {
            PurcheaseReturnVM vm = new PurcheaseReturnVM();
            
            int Id = Convert.ToInt32(PId.Split('-')[0]);
            int ProductId = Convert.ToInt32(PId.Split('-')[1]);
            Purchase pur = _repoPurchease.GetSigle(Id).Purchasevm;
            PurcheaseDetail purd= _repoPurchease.GetSigle(Id).PurcheaseDetails.FirstOrDefault(m => m.ProductId == ProductId);
      
           vm.Quantity=purd.Quantity;
           vm.UnitePrice = purd.UnitePrice;
           vm.Discount = purd.Discount;
           vm.SupplierId = pur.SupplierId;
           vm.EmployeeId = pur.EmployeeId;
           vm.PurchaseId = Id;
           
           return View("Create", vm);
        }
        [HttpPost]
        public ActionResult Create(PurcheaseReturnVM vm)
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
                mgs = result[0] + "~" + result[1];
            }
            catch (Exception ex)
            {
                mgs = result[0] + "~" + result[1];
                return View();
            }
            return View("Index");
        }
        public ActionResult Create1()
        {
            PurcheaseReturnVM vm = new PurcheaseReturnVM();
            List<PurcheaseReturnDetail> PurcheaseReturnDetailVM = new List<PurcheaseReturnDetail>();
            vm.PurcheaseReturnDetailVM = PurcheaseReturnDetailVM;
            return View( vm);
        }
        [HttpPost]
        public ActionResult Create1(PurcheaseReturnVM vm)
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
                mgs = result[0] + "~" + result[1];
            }
            catch (Exception ex)
            {
                mgs = result[0] + "~" + result[1];
                return View();
            }
            return View("Index");
        }
        public ActionResult Edit(int Id)
        {
            PurcheaseReturn vm = new PurcheaseReturn();
            vm=  _repo.GetSigle(Id);
            return View("Create",vm);
        }
        [HttpPost]
        public ActionResult Edit(PurcheaseReturnVM vm)
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
                mgs = result[0] + "~" + result[1];
            }
            catch (Exception ex)
            {
                mgs = result[0] + "~" + result[1];
                return Json(mgs, JsonRequestBehavior.AllowGet);
            }
            return View("Index");
        }
        public ActionResult GetPurcheaseDetail(string Name)
        {
            
            return Json(Name, JsonRequestBehavior.AllowGet);
        }
    }
}
