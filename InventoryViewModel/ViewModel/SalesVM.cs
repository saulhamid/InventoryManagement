using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryViewModel.Models;


namespace InventoryViewModel.ViewModel
{
   public class SalesVM:Sale
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string CustomerName { get; set; }
        public string ZoneOrAreaNae { get; set; }
        public string EmployeeNme { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Slup { get; set; }
        public decimal? UnitePrice { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? ReceiveQuantity { get; set; }
        public decimal? SalesQuantity { get; set; }
        public decimal? Replace { get; set; }
        public decimal? Return { get; set; }
        public decimal? TotalPackPrice { get; set; }
        public string Datetime { get; set; }
        public Sale Sales { get; set; }
        public IEnumerable<SalesDetail> SalesDetailvms { get; set; }

        public decimal? WithOurDiscountPrice { get; set; }
    }
}
