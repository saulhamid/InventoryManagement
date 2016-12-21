using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class ProductTypeRepo
    {
        #region Declare
        ProductTypeDAL _dal = new ProductTypeDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<ProductType> GETAllProductType { get { return _dal.GETAllProductType; } }

        public ProductType GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<ProductType> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(ProductType data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<ProductType> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<ProductType> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method
  
    }
}
