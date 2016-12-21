using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class ProductCategoryRepo
    {
        #region Declare
       ProductCategoryDal _dal = new ProductCategoryDal();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<ProductCategory> GETAllProductCategory { get { return _dal.GETAllProductCategory; } }

        public ProductCategory GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<ProductCategory> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(ProductCategory data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<ProductCategory> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<ProductCategory> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method
  
    }
}
