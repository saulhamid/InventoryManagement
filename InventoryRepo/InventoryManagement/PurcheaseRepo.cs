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
      public PurchaseVM GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
      public IEnumerable<Purchase> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
      public string[] SaveAndEdit(PurchaseVM data)
        {
            string[] result = new string[6];
            try
            {
                data.Id = _dal.GETAllPurchases().Count() +1;
                result= _dal.Save(data);
                if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

                PurcheaseDetailDAL pudDal = new PurcheaseDetailDAL();
                foreach (var item in data.PurcheaseDetails)
                {
                    item.PurchaseId = data.Id;
                    item.CreatedAt = data.CreatedAt;
                    item.CreatedAtBy = data.CreatedBy;
                    item.CreatedFrom = data.CreatedFrom;
                    result = pudDal.SaveAndEdit(item);
                }
            }
            catch (Exception ex)
            {
                result[1] = ex.Message;
                throw ex;
            }
            return result;
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
        //public List<RPTVM> rptPurchease()
        //{
        //    return _dal.rptPurchease();
        //}
         public dynamic AutocompleteInvoice(string term)
        {
            return _dal.AutocompleteInvoice(term);
        }

       
        #endregion Method

    }
}
