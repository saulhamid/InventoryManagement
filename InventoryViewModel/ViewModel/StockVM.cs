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
       public string ProductCode { get; set; }
       public string ProductName { get; set; }
       public string CustomerName { get; set; }
       public string EmployeeName { get; set; }
       public decimal TotalPackPrice { get; set; }
    }
}
