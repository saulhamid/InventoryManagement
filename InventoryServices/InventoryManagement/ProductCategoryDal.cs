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
   public class ProductCategoryDal
   {
       #region Declare

       InventoryEntities _context = new InventoryEntities();
       #endregion Declare
       #region Method
       public IEnumerable<ProductCategory> GETAllProductCategory { get { return _context.ProductCategorys.Where(m => m.IsArchive == true).AsEnumerable(); } }

       //public ProductCategory GETProductCategory { get { return _context.ProductCategorys.SingleOrDefault(); } }
       #region sigle method

       public ProductCategory GetSigle(int Id)
       {
           var result = _context.ProductCategorys.FirstOrDefault(m => m.Id == Id && m.IsArchive == true);
           return result;
       }
       public IEnumerable<ProductCategory> GETbySearch(int? Id, string name, string code)
       {
           var result = _context.ProductCategorys.Where(t => t.Code == code || t.Name == name && t.IsArchive == true).ToList();
           return result;
       }

       #endregion sigle method
       #region Save and Edit
       public string[] SaveAndEdit(ProductCategory data)
       {
           string[] result = new string[6];

           try
           {
               if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

               if (data.Id == null || data.Id == 0)
               {

                   bool duplicateCode = _context.ProductCategorys.Any(m => m.Code == data.Code);
                   if (duplicateCode == true)
                   {
                       result[1] = "Your Code is already Exit";
                       throw new ArgumentNullException("Your Code is already Exit");
                   }
                   bool duplicateName = _context.ProductCategorys.Any(m => m.Code == data.Code);
                   if (duplicateName == true)
                   {
                       result[1] = "Your Name is already Exit";
                       throw new ArgumentNullException("Your Name is already Exit");
                   }
                   data.IsActive = true;
                   data.IsArchive = false;
                   data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                   data.CreatedAt = DateTime.Now.ToString();
                   data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                   _context.ProductCategorys.Add(data);
                   result[1] = "ProductCategory Data Save";
                   result[0] = "Successfully";
               }
               else
               {
                   var duplicateCode = _context.ProductCategorys.Where(m => m.Code == data.Code && m.Id != data.Id);
                   if (duplicateCode.Count() > 0)
                   {
                       result[1] = "Your Name is already Exit";
                       throw new ArgumentNullException("Your Code is already Exit");
                   }
                   var duplicateName = _context.ProductCategorys.Where(m => m.Name == data.Name && m.Id != data.Id);
                   if (duplicateName.Count() > 0)
                   {
                       result[1] = "Your Name is already Exit";
                       throw new ArgumentNullException("Your Code is already Exit");
                   }
                   var edit = _context.ProductCategorys.Find(data.Id);
                   if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                   data.IsActive = true;
                   data.IsArchive = false;
                   data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                   data.LastUpdateBy = DateTime.Now.ToString();
                   data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                   _context.Entry(edit).CurrentValues.SetValues(data);
                   result[1] = "ProductCategory Data Update";
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
                   var data = _context.ProductCategorys.Find(Convert.ToInt32(Ids[i]));
                   data.IsArchive = false;
                   data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                   data.LastUpdateBy = DateTime.Now.ToString();
                   data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                   _context.SaveChanges();
               }
               result[1] = "ProductCategory Data Delete";
           }
           catch (Exception ex)
           {
               result[2] = ex.Message.ToString();
           }
           return result;
       }
       #endregion Delete

       public IEnumerable<ProductCategory> Dropdown()
       {
           IEnumerable<ProductCategory> ProductCategorys = from ProductCategory in _context.ProductCategorys
                                   orderby ProductCategory.Name
                                   select new ProductCategory() { Id = ProductCategory.Id, Name = ProductCategory.Name };
           return ProductCategorys.ToList();
       }
       public IEnumerable<ProductCategory> Autocomplete()
       {
           IEnumerable<ProductCategory> ProductCategorys = from ProductCategory in _context.ProductCategorys
                                                           where ProductCategory.IsArchive == false && ProductCategory.IsActive == true

                                   orderby ProductCategory.Name
                                   select new ProductCategory() { Id = ProductCategory.Id, Name = ProductCategory.Name };
           return ProductCategorys.ToList();
       }
       #endregion Method
  
    }
}
