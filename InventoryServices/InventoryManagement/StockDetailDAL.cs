using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryServices.InventoryManagement
{
    public class StockDetailDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        public string[] SaveAndEditdescrease(StockDetail data)
       {
           
           string[] result = new string[6];
           StockDetail stock = new StockDetail();
            try {
               if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
               if (stock.PurcheaseId != null ||stock.PurcheaseReturnId != null ||stock.SalesId != null ||stock.SalesReturnId != null)
               {
                   if (stock.PurcheaseId != null) {
                       stock = _context.StockDetails.FirstOrDefault(m => m.PurcheaseId == data.PurcheaseId);
                   }
                    if (stock.PurcheaseReturnId != null) {
                        stock = _context.StockDetails.FirstOrDefault(m => m.PurcheaseReturnId == data.PurcheaseReturnId);
                   }
                   if (stock.SalesId != null) {
                       stock = _context.StockDetails.FirstOrDefault(m => m.SalesId == data.SalesId);
                   }
                    if (stock.SalesReturnId != null) {
                        stock = _context.StockDetails.FirstOrDefault(m => m.SalesReturnId == data.SalesReturnId);
                   }
                    if (stock.StockQuantity < data.StockQuantity)
                   {
                       data.TransQuantity = stock.StockQuantity + data.StockQuantity;
                       data.TotalQuantity = stock.TotalQuantity + data.TransQuantity;
                   }
                   else
                   {
                       data.TransQuantity = stock.StockQuantity - data.StockQuantity;
                       data.TotalQuantity = stock.TotalQuantity - data.TransQuantity;
                   }
                    if (stock.StockDiscount < data.StockDiscount)
                   {
                       data.TransDiscount = stock.StockDiscount + data.StockDiscount;
                       data.TotalDiscount = stock.TotalDiscount + data.TransDiscount;
                   }
                   else
                   {
                       data.TransDiscount = stock.StockDiscount - data.StockDiscount;
                       data.TotalDiscount = stock.TotalDiscount - data.TransDiscount;
                   }
                    if (stock.StockPrice < data.StockPrice)
                   {
                       data.TransPrice = stock.StockPrice + data.StockPrice;
                       data.TotalPrice = stock.TotalPrice + data.TransPrice;
                   }
                   else
                   {
                       data.TransPrice = stock.StockPrice - data.StockPrice;
                       data.TotalPrice = stock.TotalPrice - data.TransPrice;
                   }
                    if (stock.StockSlup < data.StockSlup)
                   {
                       data.TransSlup = stock.StockSlup + data.StockSlup;
                       data.TotalSlup = stock.TotalSlup + data.TransSlup;
                   }
                   else
                   {
                       data.TransSlup = stock.StockSlup - data.StockSlup;
                       data.TotalSlup = stock.TotalSlup - data.TransSlup;
                   }
                    stock.Date = DateTime.Now.ToString("MM/dd/yy");
                   //_context.Entry(update).CurrentValues.SetValues(data);
                    _context.Entry(stock).State = System.Data.Entity.EntityState.Modified;
                   _context.SaveChanges();
               }
               else
               {
                   data.Date = DateTime.Now.ToString("MM/dd/yy");
                   _context.StockDetails.Add(data);
                   result[1] = "Stock Data Save";
                   result[0] = "Successfully";
               }
               _context.SaveChanges();

           }
           catch (DbEntityValidationException dbEx)
           {
               result[0] = "Fail";
               foreach ( var validationErrors in dbEx.EntityValidationErrors)
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

        public string[] SaveAndEdit(StockDetail data)
        {
            string[] result = new string[6];
            try
            {
                var sql = @"exec [dbo].[SP_StockDetail] @Option = {0},  @PurcheaseId = {1},@SalesId = {2}, @PurcheaseReturnId = {3},@Date = {5},@TotalQuantity = {6},@TotalReplace = {7},@TotalReturn = {8},
@TotalDiscount= {9},@TotalSlup= {10},,@Remarks = {11},@StockStutes = {12},@LastUpdateBy = {13},@LastUpdateAt = {14},@LastUpdateFrom = {15}";
                if (data.Id > 0)
                {
                    result[1] = _context.Database.ExecuteSqlCommand(sql, 2, data.PurcheaseId, data.SalesId, data.PurcheaseReturnId, data.Date, data.TotalQuantity, data.TotalReplace, data.TotalReturn,
                         data.TotalDiscount, data.TotalSlup, data.Remarks,
                         data.StockStutes, data.LastUpdateBy, data.LastUpdateAt, data.LastUpdateFrom).ToString();
                }
                else
                {
                    result[1] = _context.Database.ExecuteSqlCommand(sql, 1, data.PurcheaseId, data.SalesId, data.PurcheaseReturnId, data.Date, data.TotalQuantity, data.TotalReplace, data.TotalReturn,
                       data.TotalDiscount, data.TotalSlup, data.Remarks,
                       data.StockStutes, data.LastUpdateBy, data.LastUpdateAt, data.LastUpdateFrom).ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
