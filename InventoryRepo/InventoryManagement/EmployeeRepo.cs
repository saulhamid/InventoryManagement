using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
    public class EmployeeRepo
    {
        #region Declare
        EmployeeDAL _dal = new EmployeeDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<Employee> GETAllEmployee { get { return _dal.GETAllEmployee; } }

        public Employee GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<Employee> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(Employee data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<Employee> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<Employee> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method

    }
}
