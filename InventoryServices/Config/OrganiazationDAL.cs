using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryServices.Config
{
   public class OrganiazationDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        //public IEnumerable<Organization> GETAllOrganization { get { return _context.Organizations.Where(m => m.IsArchive == false).AsEnumerable(); } }
        public List<Organization> GETAllOrganization()
        {
            List<Organization> Organ = new List<Organization>();
            
            Organ = _context.Organizations.Where(m => m.IsArchive == false && m.IsActive == true).ToList();
            return Organ;
        }
        //public Organization GETOrganization { get { return _context.Organizations.SingleOrDefault(); } }
        #region sigle method

        public Organization GetSigle(int Id)
        {
            var result = _context.Organizations.FirstOrDefault(m => m.Id == Id && m.IsArchive == false);
            return result;
        }
        public IEnumerable<Organization> GETbySearch(int? Id, string name, string code)
        {
            var result = _context.Organizations.Where(t => t.Code == code || t.Name == name && t.IsArchive == false).ToList();
            return result;
        }

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(Organization data)
        {
            string[] result = new string[6];

            try
            {
            List<Organization> Organ = new List<Organization>();
                Organ=GETAllOrganization().ToList();
                if (Organ.Count() > 1) throw new ArgumentNullException("Active Organization not more then one");
                if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

                if (data.Id == 0)
                {

                    bool duplicateCode = _context.Organizations.Any(m => m.IsArchive == false && m.Code == data.Code);
                    if (duplicateCode == true)
                    {
                        result[1] = "Your Code is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    bool duplicateName = _context.Organizations.Any(m => m.IsArchive == false && m.Code == data.Code);
                    if (duplicateName == true)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Name is already Exit");
                    }

                    data.IsArchive = false;
                    data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                    data.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
                    data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Organizations.Add(data);
                    result[1] = "Organization Data Save";
                    result[0] = "Successfully";
                }
                else
                {
                    var duplicateCode = _context.Organizations.Where(m => m.IsArchive == false && m.Code == data.Code && m.Id != data.Id);
                    if (duplicateCode.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    var duplicateName = _context.Organizations.Where(m => m.IsArchive == false && m.Name == data.Name && m.Id != data.Id);
                    if (duplicateName.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    var edit = _context.Organizations.Find(data.Id);
                    if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                    data.IsArchive = false;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Entry(edit).CurrentValues.SetValues(data);
                    result[1] = "Organization Data Update";
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
                    var data = _context.Organizations.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "Organization Data Delete";
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
        public dynamic Dropdown()
        {
            var Supplier = _context.Organizations.Where(m => m.IsActive == true && m.IsArchive == false).OrderBy(m => m.Name).Select(m => new { m.Id, m.Name }).ToList();
            return Supplier;
        }
        public IEnumerable<Organization> Autocomplete(string term)
        {
            IEnumerable<Organization> Organizations = from Organization in _context.Organizations
                                    where Organization.IsArchive == false && Organization.IsActive == true
                                    && (Organization.Code.Contains(term) || Organization.Name.Contains(term))
                                    orderby Organization.Name
                                    select new Organization() { Id = Organization.Id, Name = Organization.Name };
            return Organizations.ToList();
        }
        #endregion Method
    }
}
