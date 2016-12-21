using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using InventoryViewModel.Models;
using System.Diagnostics;
using System.Web;
using System.Threading;

namespace InventoryServices.InventoryManagement
{
  public  class UOMDAL
  {
      #region Declare

      InventoryEntities _context = new InventoryEntities();
      #endregion Declare
      #region Method
      public IEnumerable<UOM> GETAllUOM { get { return _context.UOMs.Where(m => m.IsArchive == false).AsEnumerable(); } }

      //public UOM GETUOM { get { return _context.UOMs.SingleOrDefault(); } }
      #region sigle method

      public UOM GetSigle(int Id)
      {
          var result = _context.UOMs.FirstOrDefault(m=>m.Id == Id && m.IsArchive== false);
          return result;
      }
      public IEnumerable<UOM> GETbySearch(int? Id, string name, string code)
      {
          var result = _context.UOMs.Where(t => t.Code == code || t.Name == name && t.IsArchive == false).ToList();
          return result;
      }

      #endregion sigle method
      #region Save and Edit
      public string[] SaveAndEdit(UOM data)
      {
          string[] result = new string[6];

          try
          {
              if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
             
              if (data.Id == null || data.Id == 0)
              {

                  bool duplicateCode = _context.UOMs.Any(m => m.IsArchive == false && m.Code == data.Code);
                  if (duplicateCode == true)
                  {
                      result[1] = "Your Code is already Exit";
                      throw new ArgumentNullException("Your Code is already Exit");
                  }
                  bool duplicateName = _context.UOMs.Any(m => m.IsArchive == false && m.Code == data.Code);
                  if (duplicateName == true)
                  {
                      result[1] = "Your Name is already Exit";
                      throw new ArgumentNullException("Your Name is already Exit");
                  }

                  data.IsActive = data.IsActive == false ? false : true;
                  data.IsArchive = false;
                  data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                  data.CreatedAt = DateTime.Now.ToString();
                  data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                  _context.UOMs.Add(data);
                  result[1] = "UOM Data Save";
                  result[0] = "Successfully";
              }
              else
              {
                  var duplicateCode = _context.UOMs.Where(m =>m.IsArchive == false && m.Code == data.Code && m.Id != data.Id );
                  if (duplicateCode.Count() > 0)
                  {
                      result[1] = "Your Name is already Exit";
                      throw new ArgumentNullException("Your Code is already Exit");
                  }
                  var duplicateName = _context.UOMs.Where(m => m.IsArchive == false && m.Name == data.Name && m.Id != data.Id);
                  if (duplicateName.Count() > 0)
                  {
                      result[1] = "Your Name is already Exit";
                      throw new ArgumentNullException("Your Code is already Exit");
                  }
                  var edit = _context.UOMs.Find(data.Id);
                  if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                  data.IsActive = data.IsActive == false ? false : true;
                  data.IsArchive = false;
                  data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                  data.LastUpdateAt = DateTime.Now.ToString();
                  data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                  _context.Entry(edit).CurrentValues.SetValues(data);
                  result[1] = "UOM Data Update";
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
                         result[1] =" -- "+   validationErrors.Entry.Entity.GetType().FullName,
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
              for (var i = 0; i < Ids.Length; i++) {
                  var data = _context.UOMs.Find(Convert.ToInt32(Ids[i]));
                  data.IsArchive = true;
                  data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                  data.LastUpdateAt = DateTime.Now.ToString();
                  data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                  _context.SaveChanges();
              }
              result[1] = "UOM Data Delete";
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

      public IEnumerable<UOM> Dropdown() {
          IEnumerable<UOM> uoms = from uom in _context.UOMs
                                  where uom.IsArchive == false && uom.IsActive == true
                                  orderby uom.Name
                                  select new UOM() { Id=uom.Id,Name=uom.Name };
              return uoms.ToList();
      }
      public IEnumerable<UOM> Autocomplete()
      {
          IEnumerable<UOM> uoms = from uom in _context.UOMs
                                  where uom.IsArchive == false && uom.IsActive == true
                                  orderby uom.Name
                                  select new UOM() { Id = uom.Id, Name = uom.Name };
          return uoms.ToList();
      }
      #endregion Method
  }
}
