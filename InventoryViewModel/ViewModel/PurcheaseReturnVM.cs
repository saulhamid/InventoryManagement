using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
    public class PurcheaseReturnVM : PurcheaseReturn
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SupplierName { get; set; }
        public List<PurcheaseReturnDetail> PurcheaseReturnDetailVM { get; set; }
        public decimal? UnitePrice { get; set; }

        public decimal? Quantity { get; set; }
    }
}
