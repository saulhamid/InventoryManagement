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
            stock = context.Database.SqlQuery<StockVM>("exec [dbo].[SP_Purchease] @Option = {0}", 3).ToList();
            stock = (from stoc in context.Stocks


                     join purd in context.Products on stoc.ProductId equals purd.Id
                     where stoc.IsActive == true && stoc.IsArchive == false
                     select new StockVM()
                     {
                         Id = stoc.Id,
                         ProductName = purd.Name,
                         //TotalQuantity = stoc.TotalQuantity,
                         //TotalPrice = stoc.TotalQuantity * purd.UnitePrice,
                     }).ToList();
            return stock.ToList();
        }

     
        //public Stock GETStock { get { return context.Stocks.SingleOrDefault(); } }
        #region sigle method

        public Stock GetSigle(int Id)
        {
            var stock = context.Database.SqlQuery<Stock>("exec [dbo].[SP_Purchease] @Option = {0} ,@Id = {1}", 3,Id).Single();
            return stock;
        }

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(StockVM data)
        {
            string[] result = new string[6];
            try
            {
                var sql = @"exec [dbo].[SP_Purchease] @Option = {0}, @Id = {1}, @ProductId = {2}, @TotalPaid = {2},@FinalUnitPrice = {3}, @Date = {4},
@CreatedBy = {5},@CreatedAt = {6},@CreatedFrom = {7},@OpeningQuantity = {8},@Remarks = {9},@StockStutes = {10}";
                if (data.Id > 0)
                {
                    result[1] = context.Database.ExecuteSqlCommand(sql, 1, data.Id, data.ProductId, data.TotalPaid, data.FinalUnitPrice, data.Date, data.CreatedBy,
                        data.CreatedAt, data.CreatedFrom, data.OpeningQuantity, data.Remarks, data.StockStutes).ToString();

                }
                else
                {
                    result[1] = context.Database.ExecuteSqlCommand(sql, 2, data.Id, data.ProductId, data.TotalPaid, data.FinalUnitPrice, data.Date, data.CreatedBy,
                       data.CreatedAt, data.CreatedFrom, data.OpeningQuantity, data.Remarks, data.StockStutes).ToString();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        public string[] UpdateRemove(StockVM data)
        {
            string[] result = new string[6];
            try
            {
                var sql = @"exec [dbo].[SP_Purchease] @Option = {0}, @Id = {1}, @ProductId = {2}, @TotalPaid = {2},@FinalUnitPrice = {3}, @Date = {4},
@CreatedBy = {5},@CreatedAt = {6},@CreatedFrom = {7},@OpeningQuantity = {8},@Remarks = {9},@StockStutes = {10}";

                result[1] = context.Database.ExecuteSqlCommand(sql, 3, data.Id, data.ProductId, data.TotalPaid, data.FinalUnitPrice, data.Date, data.CreatedBy,
                    data.CreatedAt, data.CreatedFrom, data.OpeningQuantity, data.Remarks, data.StockStutes).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public string[] UpdateAdd(StockVM data)
        {
            string[] result = new string[6];
            try
            {
                var sql = @"exec [dbo].[SP_Purchease] @Option = {0}, @Id = {1}, @ProductId = {2}, @TotalPaid = {2},@FinalUnitPrice = {3}, @Date = {4},
@CreatedBy = {5},@CreatedAt = {6},@CreatedFrom = {7},@OpeningQuantity = {8},@Remarks = {9},@StockStutes = {10}";

                result[1] = context.Database.ExecuteSqlCommand(sql, 4, data.Id, data.ProductId, data.TotalPaid, data.FinalUnitPrice, data.Date, data.CreatedBy,
                    data.CreatedAt, data.CreatedFrom, data.OpeningQuantity, data.Remarks, data.StockStutes).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
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
                    //data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    //data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    //data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
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

