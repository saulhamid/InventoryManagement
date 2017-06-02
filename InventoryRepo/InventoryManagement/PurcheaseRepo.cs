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
    public class PurcheaseRepo
    {
        #region Declare
      PurcheaseDAL _dal = new PurcheaseDAL();
        #endregion Declare
        /******************************/
        #region Methods
      public IEnumerable<Purchase> GETAllPurchase { get { return _dal.GETAllPurchase; } }
      public List<PurchaseVM> GETAllPurchases()
      {
          return _dal.GETAllPurchases();
      }
      public PurcheaseDetailVM GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
      public IEnumerable<Purchase> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
      public string[] SaveAndEdit(PurcheaseDetailVM data)
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
        public IEnumerable<Purchase> Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        public List<RPTVM> rptPurchease()
        {
            return _dal.rptPurchease();
        }
         public dynamic AutocompleteInvoice(string term)
        {
            return _dal.AutocompleteInvoice(term);
        }
        #endregion Method

    }
}
