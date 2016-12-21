using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
  public  class ProductRepo
    {
        #region Declare
        ProductDAL _dal = new ProductDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Product> GETAllProduct { get { return _dal.GETAllProduct; } }

        public Product GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<Product> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(Product data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<Product> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<Product> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method
  
    }
}
