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
           bool check;
           string[] result = new string[6];
           StockDetail stock = new StockDetail();
           try
           {
               if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

               check = _context.StockDetails.Any(m => m.InvoiceNo == data.InvoiceNo);
               if (check == true)
               {
                   var update = _context.StockDetails.FirstOrDefault(m => m.InvoiceNo == data.InvoiceNo);
                   if (update.StockQuantity < data.StockQuantity)
                   {
                       data.TransQuantity = update.StockQuantity + data.StockQuantity;
                       data.TotalQuantity = update.TotalQuantity + data.TransQuantity;
                   }
                   else
                   {
                       data.TransQuantity = update.StockQuantity - data.StockQuantity;
                       data.TotalQuantity = update.TotalQuantity - data.TransQuantity;
                   }
                   if (update.StockDiscount < data.StockDiscount)
                   {
                       data.TransDiscount = update.StockDiscount + data.StockDiscount;
                       data.TotalDiscount = update.TotalDiscount + data.TransDiscount;
                   }
                   else
                   {
                       data.TransDiscount = update.StockDiscount - data.StockDiscount;
                       data.TotalDiscount = update.TotalDiscount - data.TransDiscount;
                   }
                   if (update.StockPrice < data.StockPrice)
                   {
                       data.TransPrice = update.StockPrice + data.StockPrice;
                       data.TotalPrice = update.TotalPrice + data.TransPrice;
                   }
                   else
                   {
                       data.TransPrice = update.StockPrice - data.StockPrice;
                       data.TotalPrice = update.TotalPrice - data.TransPrice;
                   }
                   if (update.StockSlup < data.StockSlup)
                   {
                       data.TransSlup = update.StockSlup + data.StockSlup;
                       data.TotalSlup = update.TotalSlup + data.TransSlup;
                   }
                   else
                   {
                       data.TransSlup = update.StockSlup - data.StockSlup;
                       data.TotalSlup = update.TotalSlup - data.TransSlup;
                   }
                   update.Date = DateTime.Now.ToString("MM/dd/yy");
                   //_context.Entry(update).CurrentValues.SetValues(data);
                   _context.Entry(update).State = System.Data.Entity.EntityState.Modified;
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
    }
}
