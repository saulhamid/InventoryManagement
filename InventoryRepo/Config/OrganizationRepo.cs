using InventoryServices.Config;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.Config
{
  public class OrganizationRepo
    {
        #region Declare
        OrganiazationDAL _dal = new OrganiazationDAL();
        #endregion Declare
        /******************************/
        #region Methods
        //public IEnumerable<Organization> GETAllOrganization { get { return _dal.GETAllOrganization; } }
        public List<Organization> GETAllOrganization()
        {
            return _dal.GETAllOrganization();
        }
        public Organization GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<Organization> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(Organization data)
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
        public IEnumerable<Organization> Autocomplete(string term)
        {
            return _dal.Autocomplete(term);
        }
        #endregion Method
    }
}
