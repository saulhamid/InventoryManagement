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
   public class PurcheaseReturnRepo
    {
        #region Declare
       PurcheaseReturnDAL _dal = new PurcheaseReturnDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<PurcheaseReturn> GETAllPurcheaseReturn { get { return _dal.GETAllPurcheaseReturn; } }

        public PurcheaseReturn GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public List<PurchaseVM> GETAllPurchasesByInvoice(string Invoice)
        {
            return _dal.GETAllPurchasesByInvoice(Invoice);
        }
        public List<PurcheaseReturnVM> GETAllPurcheaseReturns()
        {
            return _dal.GETAllPurcheaseReturns();
        }
        //public IEnumerable<PurcheaseReturn> GETbySearch(int? Id, string name = null, string code = null)
        //{
        //    return _dal.GETbySearch(Id, name, code);
        //}
        //public string[] SaveAndEdit(PurcheaseReturnVM data)
        //{
        //    return _dal.SaveAndEdit(data);
        //}
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        //public dynamic Dropdown()
        //{
        //    return _dal.Dropdown();
        //}
        //public IEnumerable<PurcheaseReturn> Autocomplete(string term)
        //{
        //    return _dal.Autocomplete(term);
        //}
        #endregion Method
    }
}
