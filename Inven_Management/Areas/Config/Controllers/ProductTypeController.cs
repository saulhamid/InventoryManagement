using InventoryRepo.InventoryManagement;
using InventoryViewModel.Models;
using JQueryDataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Areas.Config.Controllers
{
    public class ProductTypeController : Controller
    {
        #region Declare
        ProductTypeRepo _repo = new ProductTypeRepo();
        #endregion Declare
        public ActionResult Index()
        {
            ProductType vm = new ProductType();
            vm.IsActive = true;
            return View(vm);
        }

        //public JsonResult _index(JQueryDataTableParamModel param ) {
        //    IEnumerable<ProductType> vm = _repo.GETAllProductType;

        //    return Json(vm,JsonRequestBehavior.AllowGet);
        //}
        public ActionResult _index(JQueryDataTableParamModel param)//EmployeeId
        {
            #region Column Search
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var codeFilter = Convert.ToString(Request["sSearch_1"]);
            var nameFilter = Convert.ToString(Request["sSearch_2"]);
            var isActiveFilter = Convert.ToString(Request["sSearch_3"]);
            var RemarkFilter = Convert.ToString(Request["sSearch_4"]);

            var isActiveFilter1 = isActiveFilter.ToLower() == "y" ? true.ToString() : false.ToString();
            #endregion Column Search

            var getAllData = _repo.GETAllProductType;
            IEnumerable<ProductType> filteredData;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(param.sSearch))
            {

                //Optionally check whether the columns are searchable at all 
                var isSearchable1 = Convert.ToBoolean(Request["bSearchable_1"]);
                var isSearchable2 = Convert.ToBoolean(Request["bSearchable_2"]);
                var isSearchable3 = Convert.ToBoolean(Request["bSearchable_3"]);
                var isSearchable4 = Convert.ToBoolean(Request["bSearchable_4"]);

                filteredData = getAllData
                   .Where(c => isSearchable1 && c.Name.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isSearchable2 && c.Code.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isSearchable4 && c.Remarks.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredData = getAllData;
            }
            #region Column Filtering
            if (codeFilter != "" || nameFilter != "" || isActiveFilter != "")
            {
                filteredData = getAllData
                                .Where(c => (codeFilter == "" || c.Code.ToLower().Contains(codeFilter.ToLower()))
                                            &&
                                            (nameFilter == "" || c.Name.ToLower().Contains(nameFilter.ToLower()))
                                            &&
                                            (isActiveFilter == "" || c.IsActive.ToString().ToLower().Contains(isActiveFilter1.ToLower()))
                                            &&
                                            (RemarkFilter == "" || c.Remarks.ToLower().Contains(RemarkFilter.ToLower()))
                                        );
            }

            #endregion Column Filtering

            var isSortable_1 = Convert.ToBoolean(Request["bSortable_1"]);
            var isSortable_2 = Convert.ToBoolean(Request["bSortable_2"]);
            var isSortable_3 = Convert.ToBoolean(Request["bSortable_3"]);
            var isSortable_4 = Convert.ToBoolean(Request["bSortable_4"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<ProductType, string> orderingFunction = (c => sortColumnIndex == 1 && isSortable_1 ? c.Name :
                                                           sortColumnIndex == 2 && isSortable_2 ? c.Code :
                                                           sortColumnIndex == 3 && isSortable_3 ? c.Name :
                                                           sortColumnIndex == 3 && isSortable_4 ? c.Remarks :
                                                           "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredData = filteredData.OrderBy(orderingFunction);
            else
                filteredData = filteredData.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredData.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies
                         select new[] { Convert.ToString(c.Id),
                
                c.Code, c.Name
                , Convert.ToString(c.IsActive == true ? "Y" : "N") 
                
                ,c.Remarks };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = getAllData.Count(),
                iTotalDisplayRecords = filteredData.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Create()
        //{
        //    ProductType vm = new ProductType();
        //    return PartialView("Create", vm);
        //}
        [HttpPost]
        public ActionResult Create(ProductType vm, string IsActive)
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            ProductType vm = new ProductType();
            vm = _repo.GetSigle(Id);
            return View("Index", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductType vm)
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string Ids)
        {
            string[] a = Ids.Split('~');
            string[] result = new string[3];
            try
            {
                result = _repo.Delete(a);
                if (result[0] == "Fail")
                {
                    throw new ArgumentNullException("The expected data not found For Delete");
                }
                TempData["Msg"] = result[0] + "~" + result[1];
            }
            catch (Exception ex)
            {
                TempData["Msg"] = result[0] + "~" + result[1] + " Error: " + ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public String check(string code, string name)
        {
            var data = _repo.GETbySearch(null, name, code);
            if (data.Count() == 0)
            {
                return "That value is available!";
            }
            else
            {
                return "This value Already Exit";
            }
        }
        public JsonResult Edits(int Id)
        {
            ProductType vm = _repo.GetSigle(Id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

    }
}
