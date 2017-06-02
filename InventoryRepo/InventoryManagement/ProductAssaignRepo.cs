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
  public  class ProductAssaignRepo
    {
        #region Declare
      ProductAssaignDAL _dal = new ProductAssaignDAL();
        #endregion Declare
        /******************************/
        #region Methods
        ////public IEnumerable<ProductAssign> GETAllProductAssign { get { return _dal.GETAllProductAssign; } }
        //public List<ProductAssignDetailVM> GETAllProductAssigns()
        //{
        //    return _dal.GETAllProductAssigns();
        //}
        //public ProductAssignDetailVM GetSigle(int Id)
        //{
        //    return _dal.GetSigle(Id);
        //}
        //public IEnumerable<ProductAssign> GETbySearch(int? Id, string name = null, string code = null)
        //{
        //    return _dal.GETbySearch(Id, name, code);
        //}
      public string[] SaveAndEdit(ProductAssaignVM data)
        {
            return _dal.SaveAndEdit(data);
        }
        //public string[] Delete(string[] Ids)
        //{
        //    return _dal.Delete(Ids);
        //}
        //public dynamic Dropdown()
        //{
        //    return _dal.Dropdown();
        //}
        //public IEnumerable<ProductAssign> Autocomplete(string term)
        //{
        //    return _dal.Autocomplete(term);
        //}
        #endregion Method

    }
}
