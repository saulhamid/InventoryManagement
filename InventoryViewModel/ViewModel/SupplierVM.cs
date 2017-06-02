using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
   public class SupplierVM:Supplier
    {
        public string CountryName { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
    }
}
