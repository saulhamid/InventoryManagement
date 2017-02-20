using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryViewModel.Models;

namespace Inven_Management.Areas.InventoryManagement.Models
{
    public class PurcheaseVM:PurcheaseDetail
    {
        public Product Products { get; set; }
        public Supplier Supplier { get; set; }
    }
}