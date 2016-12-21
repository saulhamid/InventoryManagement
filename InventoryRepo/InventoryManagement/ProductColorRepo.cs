using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class ProductColorRepo
    {
        #region Declare
        ProductColorDAL _dal = new ProductColorDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<ProductColor> GETAllProductColor { get { return _dal.GETAllProductColor; } }

        public ProductColor GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<ProductColor> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(ProductColor data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<ProductColor> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<ProductColor> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method
  
    }
}
