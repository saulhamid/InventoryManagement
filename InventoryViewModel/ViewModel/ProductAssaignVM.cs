using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
   public class ProductAssaignVM
    {
        public ProductAssign proassgn { get; set; }
        public IEnumerable<ProductAssignDetail> ProductAssignDetail { get; set; }
    }
}
