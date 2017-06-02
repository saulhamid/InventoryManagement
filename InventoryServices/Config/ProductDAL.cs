using Inven_Management.Areas.Config.Models;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace InventoryServices.InventoryManagement
{
   public class ProductDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        public IEnumerable<Product> GETAllProduct { get { return _context.Products.Where(m => m.IsArchive == false).AsEnumerable(); } }

        public Product GETAllByCode(string Code) {
            //return _context.Products.FirstOrDefault(m => m.IsArchive == false && m.IsActive == true && m.Code == Code);
          Product vm=new Product();
               var prod= _context.SP_Productdetail(null, null, null, null, null, Code).Single();
            vm.Id=prod.Id;
            vm.Name = prod.Name;
            vm.Code = prod.Code;
            return vm;

        }
        public string GETProductName(int Id)
        {
            var prod = (from pro in _context.Products
                        join uom in _context.UOMs on pro.UOMId equals uom.Id
                        join ps in _context.ProductSizes on pro.ProductSizeId equals ps.Id
                        where pro.IsActive == true && pro.IsArchive == false &&
                        pro.Id.Equals(Id)
                        select new { ProductName = pro.Code + "~" + pro.Name + "(" + ps.Name + ")" + uom.Name }).Select(m=>m.ProductName).ToString();
          
            return prod;
        }
       public List<Product> GETAllProducts() {
           List<Product> vms = new List<Product>();
           Product vm = new Product();
           var prod = _context.productdetail().ToList();
           foreach (var pro in prod)
           {
               vm = new Product();
               vm.Id = pro.Id;
               vm.Name = pro.Name;
               vm.Code = pro.Code;
               vm.Discount = pro.Discount;
               vm.MinimumStock = pro.MinimumStock;
               vm.ProductBrandName = pro.BrandName;
               vm.ProductCatagoriesName = pro.CategoryName;
               vm.ProductColorName = pro.ColorName;
               vm.ProductSizeName = pro.SizeName;
               vm.UOMName = pro.UOMName;
               vm.ProductTypeName = pro.TypeName;
               vm.OpeningQuantity = pro.OpeningQuantity;
               vm.IsActive=pro.IsActive;
               vms.Add(vm);
           }
                    return vms;
        }
        #region sigle method

        public Product GetSigle(int Id)
        {
            var result = _context.Products.FirstOrDefault(m => m.Id == Id && m.IsArchive == false);
            return result;
        }
        public IEnumerable<Product> GETbySearch(int? Id, string name, string code)
        {
            var result = _context.Products.Where(t => t.Code == code || t.Name == name && t.IsArchive == false).ToList();
            return result;
        }

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(Product data)
        {
            string[] result = new string[6];

            try
            {
                if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

                if ( data.Id == 0)
                {
                    bool duplicateCode = _context.Products.Any(m => m.IsArchive == false && m.Code == data.Code);
                    if (duplicateCode == true)
                    {
                        result[1] = "Your Code is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    bool duplicateName = _context.Products.Any(m => m.IsArchive == false && m.Code == data.Code);
                    if (duplicateName == true)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Name is already Exit");
                    }
                    data.IsArchive = false;
                    data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                    data.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
                    data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Products.Add(data);
                    result[1] = "Product Data Save";
                    result[0] = "Successfully";
                }
                else
                {
                    var duplicateCode = _context.Products.Where(m => m.IsArchive == false && m.Code == data.Code && m.Id != data.Id);
                    if (duplicateCode.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    var duplicateName = _context.Products.Where(m => m.IsArchive == false && m.Name == data.Name && m.Id != data.Id);
                    if (duplicateName.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    var edit = _context.Products.Find(data.Id);
                    if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                    data.IsArchive = false;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Entry(edit).CurrentValues.SetValues(data);
                    result[1] = "Product Data Update";
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
                for (var i = 0; i < Ids.Length-1; i++)
                {
                    var data = _context.Products.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "Product Data Delete";
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
            var Product = _context.Products.Where(m => m.IsActive == true && m.IsArchive == false).OrderBy(m => m.Name).Select(m => new { m.Id, m.Name }).ToList();
            return Product;
        }
        public dynamic DropdownWithCode()
        {
            var Product = from pro in _context.Products
                          join uom in _context.UOMs on pro.UOMId equals uom.Id
                          where pro.IsActive == true && pro.IsArchive == false 
                          select (pro.Code + "-" + pro.Name + "-" + uom.Name);
            //_context.Products.Where(m => m.IsActive == true && m.IsArchive == false && (m.Code.Contains(term) || m.Name.Contains(term))).OrderBy(m => m.Name).Select(m =>m.Code +"-"+ m.Name+"("+m.UOMId).ToList();
            return Product;
        }
        public dynamic Autocomplete(string term)
        {

            var prod = from pro in _context.Products
                       join uom in _context.UOMs on pro.UOMId equals uom.Id
                       join ps in _context.ProductSizes on pro.ProductSizeId equals ps.Id
                          where pro.IsActive == true && pro.IsArchive == false &&
                          (pro.Code.Contains(term) || pro.Name.Contains(term))
                          select (pro.Code + "~" + pro.Name + "(" +ps.Name+")"+ uom.Name);
            return prod;
        }
        public object AutocompleteWithCodeName(string term)
        {
            var Product = from pro in _context.Products
                          join uom in _context.UOMs on pro.UOMId equals uom.Id
                          where pro.IsActive == true && pro.IsArchive == false &&
                          (pro.Code.Contains(term) || pro.Name.Contains(term))
                          select (pro.Code + "-" + pro.Name + "-" + uom.Name);
            //_context.Products.Where(m => m.IsActive == true && m.IsArchive == false && (m.Code.Contains(term) || m.Name.Contains(term))).OrderBy(m => m.Name).Select(m =>m.Code +"-"+ m.Name+"("+m.UOMId).ToList();
            return Product;
        }
        #endregion Method

       
    }
}
