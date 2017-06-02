using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
    public class SupplierRepo
    {
        #region Declare
        SupplierDAL _dal = new SupplierDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Supplier> GETAllSupplier { get { return _dal.GETAllSupplier; } }

        public Supplier GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<Supplier> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(Supplier data)
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
        public dynamic Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        #endregion Method

    }
}
