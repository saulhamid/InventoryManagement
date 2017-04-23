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
    public class StockController : Controller
    {
        #region Declare
        StockRepo _repo = new StockRepo();
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

            var getAllData = _repo.GETAllStockstor();
            IEnumerable<StockVM> filteredData;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);
                filteredData = getAllData.Where(c => isSearchable1 && c.EmployeeName.ToLower().Contains(param.sSearch.ToLower())
                               || isSearchable2 && c.ProductCode.ToLower().Contains(param.sSearch.ToLower())
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
                filteredData = getAllData.Where(c => (InvoiceFilter == "" || c.SupplierName.ToLower().Contains(InvoiceFilter.ToLower()))
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
                 //Convert.ToString(c.Id)
                c.ProductName+"-"+c.ProductCode
                ,c.TotalQuantity.ToString()
                ,c.FinalUnitPrice.ToString()
                ,(c.TotalPrice=c.TotalQuantity+c.FinalUnitPrice).ToString()
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
    }
}
