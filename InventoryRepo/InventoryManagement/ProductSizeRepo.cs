using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
    class ProductSizeRepo
    {
        #region Declare
        ProductSizeDAL _dal = new ProductSizeDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<ProductSize> GETAllProductSize { get { return _dal.GETAllProductSize; } }

        public ProductSize GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<ProductSize> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(ProductSize data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<ProductSize> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<ProductSize> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method
  
    }
}
