using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
    public class PurcheaseDetailVM
    {
        public Purchase Purchasevm { get; set; }
        public List<PurcheaseDetail> PurcheaseDetails { get; set; }
    }
    public class PurchaseVM:Purchase
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? TotalDiscount { get; set; }
        public List<PurcheaseDetail> PurcheaseDetails { get; set; }
    }
}
