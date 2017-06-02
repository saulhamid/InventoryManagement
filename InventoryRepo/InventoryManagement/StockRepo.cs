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
   public class StockRepo
    {
        #region Declare
        StockDAL _dal = new StockDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Stock> GETAllStock { get { return _dal.GETAllStock; } }
        public IEnumerable<StockVM> GetAllStocks()
        {
            return _dal.GetAllStocks();
        }
        public IEnumerable<StockVM> GETAllStockstor()
        {
            return _dal.GETAllStockstor();
        }
        public Stock GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public dynamic Dropdown()
        {
            return _dal.Dropdown();
        }
        public dynamic Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
         public dynamic AutocompleteInvoice(string term)
        {
            return _dal.AutocompleteInvoice(term);
        }
        #endregion Method

         public dynamic AutocompleteUnitePrice(string term)
         {
             return _dal.AutocompleteUnitePrice(term);
         }
    }
}
