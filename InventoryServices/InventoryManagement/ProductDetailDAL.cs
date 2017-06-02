using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
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
  public  class ProductDetailDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        //public IEnumerable<ProductDetail> GETAllProductDetail { get { return _context.ProductDetails.Where(m => m.IsArchive == false).AsEnumerable(); } }

        //public ProductDetail GETProductDetail { get { return _context.ProductDetails.SingleOrDefault(); } }

        public dynamic GETAllProductDetail() {

           var detail = from del in _context.ProductDetails
                                                join pro in _context.Products
                                                on del.ProductId equals pro.Id into dels from delss in dels.DefaultIfEmpty()
                                                select new {Product = delss,ProductDetail=del};
           return detail;
        }
        #region sigle method
        public ProductDetail GetSigle(int Id)
        {
            var result = _context.ProductDetails.FirstOrDefault(m => m.Id == Id && m.IsArchive == false);
            return result;
        }
        //public IEnumerable<ProductDetail> GETbySearch(int? Id, string name, string code)
        //{
        //    var result = _context.ProductDetails.Where(t => t.Code == code || t.Name == name && t.IsArchive == false).ToList();
        //    return result;
        //}

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(ProductDetail data)
        {
            string[] result = new string[6];

            try
            {
                if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

                if ( data.Id == 0)
                {
                    data.IsActive = data.IsActive == false ? false : true;
                    data.IsArchive = false;
                    data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                    data.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
                    data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.ProductDetails.Add(data);
                    result[1] = "ProductDetail Data Save";
                    result[0] = "Successfully";
                }
                else
                {
                    var edit = _context.ProductDetails.Find(data.Id);
                    if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                    data.IsActive = data.IsActive == false ? false : true;
                    data.IsArchive = false;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Entry(edit).CurrentValues.SetValues(data);
                    result[1] = "ProductDetail Data Update";
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
                    var data = _context.ProductDetails.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "ProductDetail Data Delete";
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
    
        //public IEnumerable<ProductDetail> Dropdown()
        //{
        //    IEnumerable<ProductDetail> ProductDetails = from ProductDetail in _context.ProductDetails
        //                            where ProductDetail.IsArchive == false && ProductDetail.IsActive == true
        //                            orderby ProductDetail.Id
        //                            select new ProductDetail() { Id = ProductDetail.Id, Name = ProductDetail.Name };
        //    return ProductDetails.ToList();
        //}
        //public IEnumerable<ProductDetail> Autocomplete()
        //{
        //    IEnumerable<ProductDetail> ProductDetails = from ProductDetail in _context.ProductDetails
        //                            where ProductDetail.IsArchive == false && ProductDetail.IsActive == true
        //                            orderby ProductDetail.Name
        //                            select new ProductDetail() { Id = ProductDetail.Id, Name = ProductDetail.Name };
        //    return ProductDetails.ToList();
        //}
        #endregion Method

    }
}
