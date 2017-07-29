using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
    public class ProductVM:Product
    {
        public string ProductBrandName { get; set; }
        public string ProductCatagoriesName { get; set; }
        public string ProductColorName { get; set; }
        public string ProductSizeName { get; set; }
        public string UOMName { get; set; }
        public string ProductTypeName { get; set; }
        public List<ProductType> ProductType { get; set; }
        public List<UOM> UOM { get; set; }
        public List<ProductSize> ProductSize { get; set; }
        public List<ProductColor> ProductColor { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductBrand> ProductBrands { get; set; }

    }
}
