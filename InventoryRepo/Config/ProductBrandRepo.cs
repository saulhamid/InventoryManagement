using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class ProductBrandRepo
    {
        #region Declare
        ProductBrandDAL _dal = new ProductBrandDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<ProductBrand> GETAllProductBrand { get { return _dal.GETAllProductBrand; } }

        public ProductBrand GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<ProductBrand> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(ProductBrand data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public dynamic Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<ProductBrand> Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        #endregion Method
  
    }
}
