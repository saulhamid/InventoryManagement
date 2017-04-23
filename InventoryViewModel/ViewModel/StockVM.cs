using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
   public class StockVM:Stock
    {
        public string SupplierName { get; set; }
        public int PurcheaseId { get; set; }
        public int SalesId { get; set; }
        public int PurcheaseReturnId { get; set; }
        public int SalesReturnId { get; set; }
       public string ProductCode { get; set; }
       public string ProductName { get; set; }
       public string CustomerName { get; set; }
       public string EmployeeName { get; set; }
       public decimal? UnitPrice { get; set; }
       public decimal TotalPackPrice { get; set; }
    }
}
