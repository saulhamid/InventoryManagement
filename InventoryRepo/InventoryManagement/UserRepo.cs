using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class UserRepo
    {
        #region Declare
        UserDAL _dal = new UserDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<User> GETAllUser { get { return _dal.GETAllUser; } }

        public User GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<User> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(User data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<User> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<User> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method

    }
}
