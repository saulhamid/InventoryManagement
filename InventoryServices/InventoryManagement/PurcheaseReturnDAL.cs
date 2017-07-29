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
   public class PurcheaseReturnDAL
    {
        #region Declare
        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        public IEnumerable<PurcheaseReturn> GETAllPurcheaseReturn { get { return _context.PurcheaseReturns.Where(m => m.IsArchive == false).AsEnumerable(); } }
        //public PurcheaseReturn GETPurcheaseReturn { get { return _context.PurcheaseReturns.SingleOrDefault(); } }
        public List<PurcheaseReturnVM> GETAllPurcheaseReturns()
        {
            List<PurcheaseReturnVM> vms = new List<PurcheaseReturnVM>();
            PurcheaseReturnVM vm = new PurcheaseReturnVM();
            //var prod = _context.SP_PurcheaseDetail(null, null, null, null, null, null).ToList();
            //foreach (var pro in prod)
            //{
            //    vm = new PurcheaseReturnVM();
            //    vm.Id = pro.Id;
            //    vm.ProductName = pro.ProductName;
            //    vm.InvoiceNo = pro.InvoiecNo;
            //    vm.Discount = pro.ProductDiscount;
            //    vm.UnitePrice = pro.ProdutUnitePrice;
            //    vm.Quantity = pro.ProductQuantity;
            //    vm.SupplierName = pro.SupplierName;
            //    vm.Date = pro.Date;
            //    vms.Add(vm);
            //}
            return vms;
        }
        public List<PurchaseVM> GETAllPurchasesByInvoice(string Invoice="")
        {
            List<PurchaseVM> products = new List<PurchaseVM>();

            //try
            //{
            //    PurchaseVM vm = new PurchaseVM();
            //    var pros = _context.PurcheasReturnsByInvoice("").ToList();
            //    foreach (var pross in pros)
            //    {
            //        vm = new PurchaseVM();
            //        vm.Discount = pross.Discount;
            //        vm.Id = pross.Id;
            //        vm.Quantity = pross.Quantity;
            //        vm.InvoiecNo = pross.InvoiecNo;
            //        vm.UnitPrice = pross.UnitePrice;
            //        vm.ProductCode = pross.ProductCode;
            //        vm.ProductName = pross.ProductName;
            //        vm.Date = pross.Date;
            //        vm.EmployeeName = pross.Employee;
            //        vm.SupplierName = pross.SupplierName;
                   
            //        products.Add(vm);
            //    }
            //}
            //catch (Exception ex) {
            //    return products;
            //}
            return products;
        }
        #region sigle method
        public PurcheaseReturn GetSigle(int Id)
        {
            var result = _context.PurcheaseReturns.FirstOrDefault(m => m.Id == Id && m.IsArchive == false);
            return result;
        }
        #endregion sigle method
        #region Save and Edit
        //public string[] SaveAndEdit(PurcheaseReturnVM data)
        //{
        //    string[] result = new string[6];
        //    PurcheaseReturn vm = new PurcheaseReturn();
        //    StockVM stock = new StockVM();
        //    StockDAL _dal = new StockDAL();
        //    var check = _context.Stocks.FirstOrDefault(m => m.ProductId == data.PurchaseId);
        //    if (check.TotalQuantity > 0) throw new ArgumentNullException("This Product Stock is insuficient");
        //    try
        //    {
        //        if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
        //        if (data.Id == 0)
        //        {
        //            vm.InvoiceNo = data.InvoiceNo;
        //            vm.Date = data.Date;
        //            vm.SupplierId = data.SupplierId;
        //            vm.IsActive = true;
        //            vm.IsArchive = false;
        //            vm.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
        //            vm.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
        //            vm.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
        //            _context.PurcheaseReturns.Add(vm);
        //            if (data.PurcheaseReturnDetailVM.Count() > 0 && data.PurcheaseReturnDetailVM != null)
        //            {
        //                foreach (var purdetail in data.PurcheaseReturnDetailVM)
        //                {
        //                    PurcheaseReturnDetail purvms = new PurcheaseReturnDetail();
        //                    purvms.ProductId = purdetail.ProductId;
        //                    purvms.Quantity = purdetail.Quantity;
        //                    purvms.Discount = purdetail.Discount;
        //                    purvms.TotalPrice = purdetail.TotalPrice;
        //                    purvms.CreatedAt = data.CreatedAt;
        //                    purvms.CreatedBy = data.CreatedBy;
        //                    purvms.CreatedFrom = data.CreatedFrom;
        //                    _context.PurcheaseReturnDetails.Add(purvms);
        //                    _context.SaveChanges();
        //                    stock.StockStutes = false;
        //                    stock.ProductId = purdetail.ProductId;
        //                    stock.Date = data.Date;
        //                    stock.TotalQuantity = purdetail.Quantity;
        //                    stock.TotalDiscount = purdetail.Discount;
        //                    stock.TotalPrice = purdetail.TotalPrice;
        //                    _dal.SaveAndEditUpdate(stock);
        //                }
        //            }
        //            if (result[0] == "Fail")
        //            {
        //                throw new ArgumentNullException("Stock update UNSuccessfully");
        //            }
        //            result[1] = "PurcheaseReturn Data Save";
        //            result[0] = "Successfully";
        //        }
        //        else
        //        {
        //            var edit = _context.PurcheaseReturns.Find(data.Id);
        //            if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
        //            vm.InvoiceNo = data.InvoiceNo;
        //            vm.Date = data.Date;
        //            vm.SupplierId = data.SupplierId;
        //            vm.IsActive = true;
        //            vm.IsArchive = false;
        //            vm.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
        //            vm.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
        //            vm.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
        //            _context.Entry(edit).CurrentValues.SetValues(vm);
                    
        //            if (data.PurcheaseReturnDetailVM.Count() > 0 && data.PurcheaseReturnDetailVM != null)
        //            {
        //                try
        //                {
        //                    var a = _context.PurcheaseReturnDetails.Where(m => m.PurchaseReturnId == data.Id);
        //                    _context.PurcheaseReturnDetails.RemoveRange(a);
        //                    _context.SaveChanges();
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new ArgumentNullException("the not delete");
        //                }
        //                foreach (var purdetail in data.PurcheaseReturnDetailVM)
        //                {
        //                    PurcheaseDetail purvms = new PurcheaseDetail();
        //                    purvms.ProductId = purdetail.ProductId;
                          
        //                    purvms.UnitePrice = purdetail.UnitePrice;
        //                    purvms.Quantity = purdetail.Quantity;
        //                    purvms.Discount = purdetail.Discount;
        //                    purvms.PurchaseId = data.Id;
        //                    purvms.TotalPrice = purdetail.Quantity * purdetail.UnitePrice;
        //                    purvms.LastUpdateAt = data.LastUpdateAt;
        //                    purvms.LastUpdateBy = data.LastUpdateBy;
        //                    purvms.LastUpdateFrom = data.LastUpdateFrom;
        //                    _context.PurcheaseDetails.Add(purvms);
        //                    _context.SaveChanges();
        //                    stock.ProductId = purdetail.ProductId;
        //                    stock.Date = data.Date;
        //                    stock.StockStutes = true;
        //                    stock.TotalQuantity = purdetail.Quantity;
        //                    stock.TotalDiscount = purdetail.Discount;
        //                    stock.TotalPrice = purdetail.Quantity * purdetail.UnitePrice;
        //                    _dal.SaveAndEditUpdate(stock);
        //                    _context.SaveChanges();

        //                }
        //            }
        //            stock.TotalQuantity = data.Quantity;
                 
        //            stock.TotalDiscount = data.Discount;
        //            stock.TotalPrice = data.Quantity * data.UnitePrice;
        //            result[1] = "PurcheaseReturn Data Update";
        //            result[0] = "Successfully";
        //        }
        //        _context.SaveChanges();
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
                    var data = _context.PurcheaseReturns.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "PurcheaseReturn Data Delete";
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
        //public dynamic Dropdown()
        //{
        //    var Supplier = _context.PurcheaseReturns.Where(m => m.IsActive == true && m.IsArchive == false).OrderBy(m => m.Name).Select(m => new { m.Id, m.Name }).ToList();
        //    return Supplier;
        //}
        //public IEnumerable<PurcheaseReturn> Dropdown()
        //{
        //    IEnumerable<PurcheaseReturn> PurcheaseReturns = from PurcheaseReturn in _context.PurcheaseReturns
        //                                                    where PurcheaseReturn.IsArchive == false && PurcheaseReturn.IsActive == true
        //                                                    orderby PurcheaseReturn.Name
        //                                                    select new PurcheaseReturn() { Id = PurcheaseReturn.Id, Name = PurcheaseReturn.Name };
        //    return PurcheaseReturns.ToList();
        //}
        //public IEnumerable<PurcheaseReturn> Autocomplete(string term)
        //{
        //    IEnumerable<PurcheaseReturn> PurcheaseReturns = from PurcheaseReturn in _context.PurcheaseReturns
        //                            where PurcheaseReturn.IsArchive == false && PurcheaseReturn.IsActive == true
        //                            && (PurcheaseReturn.Code.Contains(term) || PurcheaseReturn.Name.Contains(term))
        //                            orderby PurcheaseReturn.Name
        //                            select new PurcheaseReturn() { Id = PurcheaseReturn.Id, Name = PurcheaseReturn.Name };
        //    return PurcheaseReturns.ToList();
        //}
        #endregion Method
    }
}
