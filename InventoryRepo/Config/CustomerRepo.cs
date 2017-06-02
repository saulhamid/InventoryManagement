using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class CustomerRepo
    {
        #region Declare
        CustomerDAL _dal = new CustomerDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Customer> GETAllCustomer { get { return _dal.GETAllCustomer; } }

        public Customer GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<Customer> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(Customer data)
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
        public IEnumerable<Customer> Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        #endregion Method
  
    }
}
