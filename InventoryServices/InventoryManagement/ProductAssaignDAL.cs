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
   public class ProductAssaignDAL
    {
        #region Declare
        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        public IEnumerable<Purchase> GETAllPurchase { get { return _context.Purchases.Where(m => m.IsArchive == false).AsEnumerable(); } }
        #region sigle method

        public IEnumerable<Purchase> GETbySearch(int? Id, string name, string InvoiecNo)
        {
            var result = _context.Purchases.Where(t => t.InvoiecNo == InvoiecNo && t.IsArchive == false).ToList();
            return result;
        }
        #endregion sigle method
        #region Save and Edit
        //public string[] SaveAndEdit(ProductAssaignVM data)
        //{
        //    StockDAL _dal = new StockDAL();
        //    StockVM stock = new StockVM();
        //    string[] result = new string[6];
        //    try
        //    {
        //        try
        //        {
        //            if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
        //            if (data.proassgn.Id ==0)
        //            {                        data.proassgn.Id =_context.ProductAssigns.Count() + 1;
        //                data.proassgn.IsActive = data.proassgn.IsActive == false ? false : true;
        //                //data.Date = (Convert.ToDateTime(data.Date).ToString("MM/dd/yy")).ToString();
        //                data.proassgn.IsArchive = false;
        //                data.proassgn.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
        //                data.proassgn.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
        //                data.proassgn.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
        //                _context.ProductAssigns.Add(data.proassgn);

        //                ProductAssignDetail purvms = new ProductAssignDetail();
        //                if (data.ProductAssignDetail.Count() > 0 && data.ProductAssignDetail != null)
        //                {
        //                    foreach (var purdetail in data.ProductAssignDetail)
        //                    {
        //                        purvms.ProductId = purdetail.Id;
        //                        purvms.Name = purdetail.Name;
        //                        purvms.Code = purdetail.Code;
        //                        purvms.UnitePrice = purdetail.UnitePrice;
        //                        purvms.Quantity = purdetail.Quantity;
        //                        purvms.Discount = purdetail.Discount;
        //                        purvms.ProductAssignId = data.proassgn.Id;
        //                        purvms.CreatedAt = data.proassgn.CreatedAt;
        //                        purvms.CreatedBy = data.proassgn.CreatedBy;
        //                        purvms.CreatedFrom = data.proassgn.CreatedFrom;
        //                        _context.ProductAssignDetails.Add(purvms);
        //                        _context.SaveChanges();
        //                        stock.StockStutes = false;
                                
        //                        stock.TotalDiscount = purdetail.Discount;
        //                        stock.TotalPrice = purdetail.Quantity * purdetail.UnitePrice;
        //                        _dal.SaveAndEditUpdate(stock);
        //                    }
        //                }
        //                result[1] = "Purchase Data Save";
        //                result[0] = "Successfully";
        //            }
        //            else
        //            {

        //                var edit = _context.ProductAssigns.Find(data.proassgn.Id);
        //                if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
        //                //data.Date = (Convert.ToDateTime(data.Date).ToString("MM/dd/yy")).ToString();
        //                data.proassgn.IsActive = data.proassgn.IsActive == false ? false : true;
        //                data.proassgn.IsArchive = false;
        //                data.proassgn.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
        //                data.proassgn.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
        //                data.proassgn.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
        //                _context.Entry(edit).CurrentValues.SetValues(data.proassgn);
        //                result[1] = "Purchase Data Update";
        //                result[0] = "Successfully";
        //                _context.SaveChanges();
        //                if (data.ProductAssignDetail.Count() > 0 && data.ProductAssignDetail != null)
        //                {
        //                    foreach (var purdetail in data.ProductAssignDetail)
        //                    {
        //                        ProductAssignDetail proassd = _context.ProductAssignDetails.FirstOrDefault(m => m.ProductAssignId == data.proassgn.Id);
        //                        ProductAssignDetail purvms = new ProductAssignDetail();
        //                        purvms.Replace = purdetail.Returns;
        //                        purvms.SalesQuantity = proassd.Quantity - purdetail.Returns-purdetail.Returns;
        //                        purvms.Replace = proassd.Quantity - proassd.SalesQuantity-proassd.Returns;
        //                        purvms.LastUpdateAt = data.proassgn.LastUpdateAt;
        //                        purvms.LastUpdateBy = data.proassgn.LastUpdateBy;
        //                        purvms.LastUpdateFrom = data.proassgn.LastUpdateFrom;
        //                        _context.Entry(proassd).CurrentValues.SetValues(data.proassgn);
        //                        _context.SaveChanges();
        //                        stock.TotalDiscount = purdetail.Discount;
        //                        stock.TotalPrice = purdetail.Quantity * purdetail.UnitePrice;
        //                        stock.StockStutes = false;
        //                        _dal.SaveAndEditUpdate(stock);
        //                        _context.SaveChanges();
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result[1] = ex.Message;
        //        }
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        result[0] = "Fail";
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation(
        //                      "Class: {0}, Property: {1}, Error: {2}",
        //                   result[1] = " -- " + validationErrors.Entry.Entity.GetType().FullName,
        //                     result[1] += " -- " + validationError.PropertyName,
        //                    result[1] += validationError.ErrorMessage);
        //            }
        //        }
        //        result[0] = "Fail";
        //    }
        //    _context.SaveChanges();
        //    return result;
        //}
        #endregion Save and Edit
        #region Delete
        public string[] Delete(string[] Ids)
        {
            string[] result = new string[3];
            try
            {
                for (var i = 0; i < Ids.Length; i++)
                {
                    var data = _context.Purchases.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "Purchase Data Delete";
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
            var Supplier = _context.Purchases.Where(m => m.IsActive == true && m.IsArchive == false).Select(m => new { m.Id }).ToList();
            return Supplier;
        }
        public IEnumerable<Purchase> Autocomplete(string term)
        {
            IEnumerable<Purchase> Purchases = from Purchase in _context.Purchases
                                              where Purchase.IsArchive == false && Purchase.IsActive == true
                                              && (Purchase.InvoiecNo.Contains(term))
                                              orderby Purchase.InvoiecNo
                                              select new Purchase { Id = Purchase.Id };
            return Purchases.ToList();
        }
        #endregion Method

    }
}
