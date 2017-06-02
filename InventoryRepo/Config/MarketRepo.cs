using InventoryServices.Config;
using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.Config
{
   public class MarketRepo
    {

        #region Declare
        MarketDAL _dal = new MarketDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Market> GETAllMarket { get { return _dal.GETAllMarket; } }

        public Market GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<Market> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(Market data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public dynamic Dropdown(int Id)
        {
            return _dal.Dropdown(Id);
        }
        public IEnumerable<Market> Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        #endregion Method
    }
}
