using Inven_Management.Areas.Config.Models;
using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
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
        public dynamic GETAllByCode(string Code)
        {
            return _dal.GETAllByCode(Code);
        }
        public IEnumerable<ProductVM> GETAllProducts()
        {
            return _dal.GETAllProducts();
        }
       public string GETProductName(int Id)
        {
            return _dal.GETProductName(Id);
        }
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
        public dynamic Dropdown()
        {
            return _dal.Dropdown();
        }
        public dynamic DropdownWithCode()
        {
            return _dal.DropdownWithCode();
        }
        public dynamic Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        public object AutocompleteWithCodeName(string term)
        {
            return _dal.AutocompleteWithCodeName(term);
        }
  
        #endregion Method
    }
}
