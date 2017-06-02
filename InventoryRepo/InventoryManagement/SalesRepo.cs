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
   public class SalesRepo
    {
        #region Declare
        SalesDAL _dal = new SalesDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Sale> GETAllSale { get { return _dal.GETAllSale; } }
        public List<SalesVM> GETAllSales()
        {
            return _dal.GETAllSales();
        }
        public SalesVM GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        //public IEnumerable<Sales> GETbySearch(int? Id, string name = null, string code = null)
        //{
        //    return _dal.GETbySearch(Id, name, code);
        //}
        public string[] SaveAndEdit(SalesVM data)
        {
            return _dal.SaveAndEdit(data);
        }
        public List<RPTVM> rptSales()
       {
           return _dal.rptSales();
       }
        //public string[] Delete(string[] Ids)
        //{
        //    return _dal.Delete(Ids);
        //}
        //public dynamic Dropdown()
        //{
        //    return _dal.Dropdown();
        //}
        //public IEnumerable<Sales> Autocomplete(string term)
        //{
        //    return _dal.Autocomplete(term);
        //}
        #endregion Method
    }
}
