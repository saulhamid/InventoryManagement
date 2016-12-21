using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryServices.InventoryManagement
{
  public  class UserDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        public IEnumerable<User> GETAllUser { get { return _context.Users.Where(m=>m.IsActive == true).AsEnumerable(); } }

        //public User GETUser { get { return _context.Users.SingleOrDefault(); } }
        #region sigle method

        public User GetSigle(int Id)
        {
            var result = _context.Users.FirstOrDefault(m => m.Id == Id );
            return result;
        }
        public IEnumerable<User> GETbySearch(int? Id, string username, string password)
        {
            var result = _context.Users.Where(t => t.UserName.Trim().ToLower() == username.Trim().ToLower() || t.Password.Trim().ToLower() == password.Trim().ToLower()).ToList();
            return result;
        }

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(User data)
        {
            string[] result = new string[6];

            try
            {
                if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

                if (data.Id == null || data.Id == 0)
                {

                    bool duplicateUserName = _context.Users.Any( m=>m.UserName == data.UserName);
                    if (duplicateUserName == true)
                    {
                        result[1] = "Your UserName is already Exit";
                        throw new ArgumentNullException("Your UserName is already Exit");
                    }
                    

                    data.IsActive = data.IsActive == false ? false : true;
                    data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                    data.CreatedAt = DateTime.Now.ToString();
                    data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Users.Add(data);
                    result[1] = "User Data Save";
                    result[0] = "Successfully";
                }
                else
                {
                    var duplicateUserName = _context.Users.Where( m=>m.UserName == data.UserName && m.Id != data.Id);
                    if (duplicateUserName.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your UserName is already Exit");
                    }
                   
                    var edit = _context.Users.Find(data.Id);
                    if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                    data.IsActive = data.IsActive == false ? false : true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString();
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Entry(edit).CurrentValues.SetValues(data);
                    result[1] = "User Data Update";
                    result[0] = "Successfully";
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                result[0] = "Fail";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation(
                              "Class: {0}, Property: {1}, Error: {2}",
                           result[1] = " -- " + validationErrors.Entry.Entity.GetType().FullName,
                             result[1] += " -- " + validationError.PropertyName,
                            result[1] += validationError.ErrorMessage);
                    }
                }

                //result[2] = ex.Message.ToString();
                result[0] = "Fail";
            }

            return result;
        }
        #endregion Save and Edit

        #region Delete
        public string[] Delete(string[] Ids)
        {
            string[] result = new string[3];
            try
            {
                for (var i = 0; i < Ids.Length; i++)
                {
                    var data = _context.Users.Find(Convert.ToInt32(Ids[i]));
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString();
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "User Data Delete";
            }
            catch (Exception ex)
            {
                result[2] = ex.Message.ToString();
            }
            finally
            {
                result[0] = "Successfully";
            }
            return result;
        }
        #endregion Delete

        public IEnumerable<User> Dropdown()
        {
            IEnumerable<User> Users = from User in _context.Users
                                    where  User.IsActive == true
                                    orderby User.UserName
                                      select new User() { Id = User.Id, UserName = User.UserName };
            return Users.ToList();
        }
        public IEnumerable<User> Autocomplete()
        {
            IEnumerable<User> Users = from User in _context.Users
                                    where  User.IsActive == true
                                    orderby User.UserName
                                      select new User() { Id = User.Id, UserName = User.UserName };
            return Users.ToList();
        }
        #endregion Method

    }
}
