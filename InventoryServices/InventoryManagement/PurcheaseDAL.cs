using Inven_Management.Areas.Config.Models;
using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;
using InventoryServices.Config;
namespace InventoryServices.InventoryManagement
{
  public  class PurcheaseDAL
    {
        #region Declare
        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        public IEnumerable<Purchase> GETAllPurchase { get { return _context.Purchases.Where(m => m.IsArchive == false).AsEnumerable(); } }
        #region sigle method
        public List<PurchaseVM> GETAllPurchases()
        {
            List<PurchaseVM> products = new List<PurchaseVM>();
            products = _context.Database.SqlQuery<PurchaseVM>("exec [dbo].[SP_PurcheaseIndex]").ToList();
           PurchaseVM vm = new PurchaseVM();
           //var pros = _context.SP_PurcheaseIndex(null, null, null, null, null).ToList();
           // foreach (var pross in pros) {
           //     vm = new PurchaseVM();
           //     vm.Date = pross.Date;
           //     vm.Id = pross.Id;
           //     vm.TotalDiscount = pross.ProductDiscount;
           //     vm.TotalPrice = pross.TotalAmount;
           //     vm.GrandTotal = pross.TotalAmount;
           //     vm.InvoiecNo = pross.InvoiecNo;
           //     vm.EmployeeName = pross.EmployeeName;
           //     vm.SupplierName = pross.SupplierName;
           //     products.Add(vm);
           // }
            
            return products;
        }
       
        public PurcheaseDetailVM GetSigle(int Id)
        {
            PurcheaseDetailVM purdvm = new PurcheaseDetailVM();
            PurcheaseDetail pudvm = new PurcheaseDetail();
            List<PurcheaseDetail> PurcheaseDetails = new List<PurcheaseDetail>();
            purdvm.Purchasevm = _context.Purchases.FirstOrDefault(m=>m.Id == Id);
           
                var prod= _context.SP_PurcheaseDetail(Id, null, null, null, null, null).ToList();
                foreach (var item in prod) {
                    pudvm = new PurcheaseDetail();
                    pudvm.ProductName = item.ProductName;
                    pudvm.ProductName =item.ProductCode+"~"+item.ProductName+"("+item.ProductSize+")"+item.UOMName;
                    pudvm.Quantity = item.ProductQuantity;
                    pudvm.UnitePrice = item.ProdutUnitePrice;
                    pudvm.Discount = item.ProductDiscount;
                    PurcheaseDetails.Add(pudvm);
                }
                purdvm.PurcheaseDetails = PurcheaseDetails;
            return purdvm;
        }
        public IEnumerable<Purchase> GETbySearch(int? Id, string name, string code)
        {
            var result = _context.Purchases.Where(t => t.InvoiecNo == code && t.IsArchive == false).ToList();
            return result;
        }
        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(PurcheaseDetailVM data)
        {
            StockDAL _dal = new StockDAL();
            StockVM stock = new StockVM();
            string[] result = new string[6];
                try
                {
                    try
                    {  
                    if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
                    if (data.Purchasevm.Id == 0)
                    {
                        bool duplicateName = _context.Purchases.Any(m => m.IsArchive == false && m.InvoiecNo == data.Purchasevm.InvoiecNo );
                        if (duplicateName == true)
                        {
                            result[1] = "Your InvoiecNo is already Exit";
                            throw new ArgumentNullException("Your InvoiecNo is already Exit");
                        }
                        data.Purchasevm.Id = GETAllPurchase.Count()+111;
                        data.Purchasevm.IsActive = data.Purchasevm.IsActive == false ? false : true;
                        data.Purchasevm.Date =(Convert.ToDateTime(data.Purchasevm.Date).ToString("MM/dd/yy")).ToString();
                        data.Purchasevm.IsArchive = false;
                        data.Purchasevm.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                        data.Purchasevm.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
                        data.Purchasevm.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                        _context.Purchases.Add(data.Purchasevm);

                        if (data.PurcheaseDetails.Count() > 0 && data.PurcheaseDetails != null)
                            {
                                foreach (var edit in data.PurcheaseDetails)
                                {
                                    PurcheaseDetail purvms = new PurcheaseDetail();
                                    purvms.ProductId = edit.ProductId;
                                  
                                    purvms.UnitePrice = edit.UnitePrice;
                                    purvms.Quantity = edit.Quantity;
                                    purvms.Discount = edit.Discount;
                                    purvms.TotalPrice = edit.Quantity * edit.UnitePrice;
                                    purvms.PurchaseId = data.Purchasevm.Id;
                                    purvms.CreatedAt = data.Purchasevm.CreatedAt;
                                    purvms.CreatedAtBy = data.Purchasevm.CreatedBy;
                                    purvms.CreatedFrom = data.Purchasevm.CreatedFrom;
                                    _context.PurcheaseDetails.Add(purvms);
                                    _context.SaveChanges();
                                    _context.Database.ExecuteSqlCommand(@"exec [SP_StockUpdateAdd] @ProductId={0},@PurcheaseId={1},
                                    @Date=={2},@TotalQuantity={3},@FinalUnitPrice={4},@StockStutes={5},@Remarks={6},@LastUpdateBy={7},@LastUpdateAt={8},@LastUpdateFrom={9}",
                                       edit.ProductId, data.Purchasevm.Id, data.Purchasevm.Date, edit.UnitePrice, true, edit.Remarks, data.Purchasevm.CreatedAt
                                       , data.Purchasevm.CreatedBy, data.Purchasevm.CreatedFrom);
                                }
                            }
                        result[1] = "Purchase Data Save";
                        result[0] = "Successfully";
                        _context.SaveChanges();
                    }
                    else
                    {
                        var duplicateName = _context.Purchases.Where(m => m.IsArchive == false && m.InvoiecNo == data.Purchasevm.InvoiecNo && m.Id != data.Purchasevm.Id);
                        if (duplicateName.Count() > 0)
                        {
                            result[1] = "Your InvoiecNo is already Exit";
                            throw new ArgumentNullException("Your InvoiecNo is already Exit");
                        }
                        var edit = _context.Purchases.Find(data.Purchasevm.Id);
                        if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                        data.Purchasevm.Date = (Convert.ToDateTime(data.Purchasevm.Date).ToString("MM/dd/yy")).ToString();
                        data.Purchasevm.IsActive = data.Purchasevm.IsActive == false ? false : true;
                        data.Purchasevm.IsArchive = false;
                        data.Purchasevm.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                        data.Purchasevm.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                        data.Purchasevm.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();

                        _context.Entry(edit).CurrentValues.SetValues(data.Purchasevm);
                        result[1] = "Purchase Data Update";
                        result[0] = "Successfully";
                        _context.SaveChanges();

                        if (data.PurcheaseDetails.Count() > 0 && data.PurcheaseDetails != null)
                        {
                            try
                            {
                                foreach (var edit1 in data.PurcheaseDetails)
                            {
                                _context.Database.ExecuteSqlCommand(@"exec [SP_StockUpdateAdd] @ProductId={0},
                                    @TotalQuantity={1}",
                                      edit1.ProductId,edit1.Quantity);
                                }
                             var a=   _context.PurcheaseDetails.Where(m=>m.PurchaseId == data.Purchasevm.Id);
                             _context.PurcheaseDetails.RemoveRange(a);
                                _context.SaveChanges();
                            }
                            catch (Exception ex) {
                                throw new ArgumentNullException("the not delete");
                            }
                            foreach (var edit1 in data.PurcheaseDetails)
                            {
                                PurcheaseDetail purvms = new PurcheaseDetail();
                                purvms.ProductId = edit1.ProductId;
                                purvms.UnitePrice = edit1.UnitePrice;
                                purvms.Quantity = edit1.Quantity;
                                purvms.Discount = edit1.Discount;
                                purvms.PurchaseId = data.Purchasevm.Id;
                                purvms.TotalPrice = edit1.Quantity * edit1.UnitePrice;
                                purvms.LastUpdateAt = data.Purchasevm.LastUpdateAt;
                                purvms.LastUpdateBy = data.Purchasevm.LastUpdateBy;
                                purvms.LastUpdateFrom = data.Purchasevm.LastUpdateFrom;
                                _context.PurcheaseDetails.Add(purvms);
                                _context.SaveChanges();
                                _context.Database.ExecuteSqlCommand(@"exec [SP_StockUpdateAdd] @ProductId={0},@PurcheaseId={1},
                                    @Date=={2},@TotalQuantity={3},@FinalUnitPrice={4},@StockStutes={5},@Remarks={6},@LastUpdateBy={7},@LastUpdateAt={8},@LastUpdateFrom={9}",
                                        edit1.ProductId, data.Purchasevm.Id, data.Purchasevm.Date,edit1.Quantity, edit1.UnitePrice, true, edit1.Remarks, data.Purchasevm.CreatedAt
                                        , data.Purchasevm.CreatedBy, data.Purchasevm.CreatedFrom);
                            }
                        }
                    }
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
                        result[0] = "Fail";
                    }
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
                    result[0] = "Fail";
                }
                _context.SaveChanges();            
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
                    var b = Convert.ToInt32(Ids[i]);
                    var data = _context.Purchases.Find(b);
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                    
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    
                    var c= Convert.ToInt32(Ids[i]);
                    var a = _context.PurcheaseDetails.Where(m => m.PurchaseId ==c).ToList();
                    _context.PurcheaseDetails.RemoveRange(a);
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
            var Supplier = _context.Purchases.Where(m => m.IsActive == true && m.IsArchive == false).Select(m => new { m.Id}).ToList();
            return Supplier;
        }
        public IEnumerable<Purchase> Autocomplete(string term)
        {
            IEnumerable<Purchase> Purchases = from Purchase in _context.Purchases
                                    where Purchase.IsArchive == false && Purchase.IsActive == true
                                    && (Purchase.InvoiecNo.Contains(term))
                                    orderby Purchase.InvoiecNo
                                    select new Purchase { Id = Purchase.Id};
            return Purchases.ToList();
        }
        public List<RPTVM> rptPurchease()
        {
            List<RPTVM> vms = new List<RPTVM>();
            RPTVM vm = new RPTVM();
            OrganiazationDAL orgDal = new OrganiazationDAL();
            Organization org = new Organization();
            org = orgDal.GETAllOrganization().FirstOrDefault(); ;
            var pur = _context.SP_PurcheaseDetail(null,null,null,null,null,null).ToList();
            foreach (var pp in pur)
            {
                vm = new RPTVM();
                vm.Id = pp.Id;
                vm.InvoiecNo = pp.InvoiecNo;
                vm.Date = Convert.ToDateTime(pp.Date);
                vm.SupplierName = pp.SupplierName;
                vm.SupplierMobile = pp.SupplierMobile;
                vm.SupplierEmail = pp.SupplierEmail;
                vm.SupplierPresentAddress = pp.SupplierPresentAddress;
                vm.EmployeeId = pp.EmployeeId;
                vm.EmployeeCode = pp.EmployeeCode;
                vm.EmployeeName = pp.EmployeeName;
                vm.EmployeeMobile = pp.EmployeeMobile;
                vm.EmployeeEmail = pp.EmployeeEmail;
                vm.EmployeePresentAddress = pp.EmployeePresentAddress;
                vm.ProductId = pp.ProductId;
                vm.ProductCode = pp.ProductCode;
                vm.ProductName = pp.ProductName;
                vm.ProductDiscount = pp.ProductDiscount;
                vm.ProductQuantity = pp.ProductQuantity;
                vm.UOMName = pp.UOMName;
                vm.ProductSize = pp.ProductSize;
                vm.ProdutUnitePrice = pp.ProdutUnitePrice;
                vm.OrganizationId = org.Id;
                vm.OrganizationCode = org.Code;
                vm.OrganizationName = org.Name;
                vm.OrganizationMobile = org.Mobile;
                vm.OrganizationEmail = org.Email;
                vm.OrganizationPresentAddress = org.PresentAddress;

                vms.Add(vm);
            }
            return vms;
        }
        public dynamic AutocompleteInvoice(string term)
        {
            var pro = _context.Purchases.Where(m => m.IsArchive == false && m.IsActive == true
                         && (m.InvoiecNo.Contains(term) || m.InvoiecNo.Contains(term))).OrderBy(m => m.InvoiecNo).Select(m => m.InvoiecNo).ToList();
            return pro;
        }
        #endregion Method
    }
}
