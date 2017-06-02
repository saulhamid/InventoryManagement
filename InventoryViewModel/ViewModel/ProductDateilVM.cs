using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inven_Management.Areas.Config.Models
{
    public class ProductDateilVM
    {
        public Product Products { get; set; }
        public ProductType ProductTypes { get; set; }
        public ProductBrand ProductBrands { get; set; }
        public ProductSize ProductSizes { get; set; }
        public ProductColor ProductColors { get; set; }
        public ProductCategory ProductCategorys { get; set; }
        public Supplier Suppliers { get; set; }
        public UOM UOMs { get; set; }
    }
}