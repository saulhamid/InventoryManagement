using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace InventoryServices.InventoryManagement
{
    public class StockDAL
    {
        #region Declare

        InventoryEntities context = new InventoryEntities();
        StockDetailDAL _repostockd = new StockDetailDAL();
        StockDetail stockd = new StockDetail();
        #endregion Declare
        #region Method
        public IEnumerable<Stock> GETAllStock { get { return context.Stocks.AsEnumerable(); } }
        //public List<Stock> GETAllStockss { get { return context.sp_stockdetails().ToList<Stock>; } }
        public IEnumerable<StockVM> GETAllStockstor()
        {
            List<StockVM> stock = new List<StockVM>();
            StockVM vm = new StockVM();
            var pros = context.Database.SqlQuery<StockVM>("exec [SP_Stocks]").ToList();

            //var abb = context.SP_StockDetail(null);

            return pros;
        }
        public List<StockVM> GetAllStocks()
        {
            List<StockVM> stock = new List<StockVM>();

            stock = (from stoc in context.Stocks


                     join purd in context.Products on stoc.ProductId equals purd.Id
                     where stoc.IsActive == true && stoc.IsArchive == false
                     select new StockVM()
                     {
                         Id = stoc.Id,
                         ProductName = purd.Name,
                         TotalQuantity = stoc.TotalQuantity,
                         TotalPrice = stoc.TotalQuantity * purd.UnitePrice,
                     }).ToList();
            return stock.ToList();

        }
        public List<PurchaseVM> GETAllPurchases()
        {
            List<PurchaseVM> products = new List<PurchaseVM>();

            products = (from pur in context.Purchases
                        join supp in context.Suppliers on pur.SupplierId equals supp.Id
                        join emp in context.Employees on pur.EmployeeId equals emp.Id
                        join purd in context.PurcheaseDetails on pur.Id equals purd.PurchaseId
                        where pur.IsActive == true && pur.IsArchive == false
                        select new PurchaseVM()
                        {
                            Id = pur.Id,
                            InvoiecNo = pur.InvoiecNo,
                            ProductName = purd.ProductName,
                            Quantity = purd.Quantity,
                            UnitPrice = purd.UnitePrice,
                            TotalPrice = purd.Quantity * purd.UnitePrice,
                            Discount = purd.Discount,
                            Date = pur.Date,
                            SupplierName = supp.Name,
                            EmployeeName = emp.Name
                        }).ToList();
            return products.ToList();
        }
        //public Stock GETStock { get { return context.Stocks.SingleOrDefault(); } }
        #region sigle method

        public Stock GetSigle(int Id)
        {
            var result = context.Stocks.FirstOrDefault(m => m.Id == Id && m.IsArchive == false && m.IsActive == true);
            return result;
        }

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEditUpdate(StockVM data)
        {
            bool check;
            string[] result = new string[6];
            Stock stock = new Stock();
            StockDetail stockdetail = new StockDetail();
            try
            {

                context.Database.ExecuteSqlCommand("");

            }
            catch (DbEntityValidationException dbEx)
            {
                result[0] = "Fail";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation(
                              "Class: {0}, Property: {1}, Error: {2}",
                           result[1] = " -- " + validationErrors.Entry.Entity.GetType().FullName,
                             result[1] += " -- " + validationError.PropertyName,
                            result[1] += validationError.ErrorMessage);
                    }
                }

                //result[2] = ex.Message.ToString();
                result[0] = "Fail";
            }

            return result;
        }



        #endregion Save and Edit

        #region Delete
        public string[] Delete(string[] Ids)
        {
            string[] result = new string[3];
            try
            {
                for (var i = 0; i < Ids.Length; i++)
                {
                    var data = context.Stocks.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    context.SaveChanges();
                }
                result[1] = "Stock Data Delete";
            }
            catch (Exception ex)
            {
                result[2] = ex.Message.ToString();
            }
            finally
            {
                result[0] = "Successfully";
            }
            return result;
        }
        #endregion Delete

        public dynamic Dropdown()
        {
            var Stocks = from St in context.Stocks
                         join pp in context.Products on St.ProductId equals pp.Id
                         where St.IsArchive == false && St.IsActive == true
                         orderby pp.Name
                         select new { Id = pp.Id, Name = pp.Name };
            return Stocks.ToList();
        }
        public dynamic Autocomplete(string term)
        {
            var Stocks = from Stock in context.Stocks

                         select new Stock() { Id = Stock.Id };
            return Stocks.ToList();
        }
        public dynamic AutocompleteInvoice(string term)
        {
            var Stocks = from Stock in context.Stocks
                         where Stock.IsArchive == false && Stock.IsActive == true

                         select new Stock() { Id = Stock.Id };
            return Stocks.ToList();
        }
        #endregion Method


        public dynamic AutocompleteUnitePrice(string term)
        {
            var Product = from st in context.Stocks
                          where st.IsActive == true && st.IsArchive == false

                          select st;
            //context.Products.Where(m => m.IsActive == true && m.IsArchive == false && (m.Code.Contains(term) || m.Name.Contains(term))).OrderBy(m => m.Name).Select(m =>m.Code +"-"+ m.Name+"("+m.UOMId).ToList();
            return Product;
        }
    }
}

