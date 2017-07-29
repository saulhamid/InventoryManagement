using InventoryViewModel.Models;
using InventoryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
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
            products = _context.Database.SqlQuery<PurchaseVM>("exec [dbo].[SP_Purchease] @Option = {0}",3).ToList();
            return products;
        }
       
        public PurchaseVM GetSigle(int Id)
        {
           PurchaseVM products =new PurchaseVM();
            products = _context.Database.SqlQuery<PurchaseVM>("exec [dbo].[SP_Purchease] @Option = {0},@Id = {1} ", 4,Id).Single();
          
            //PurcheaseDetail pudvm = new PurcheaseDetail();
            //List<PurcheaseDetail> PurcheaseDetails = new List<PurcheaseDetail>();
            //purdvm.Purchasevm = _context.Purchases.FirstOrDefault(m=>m.Id == Id);
           
            //    var prod= _context.SP_PurcheaseDetail(Id, null, null, null, null, null).ToList();
            //    foreach (var item in prod) {
            //        pudvm = new PurcheaseDetail();
            //        pudvm.ProductName = item.ProductName;
            //        pudvm.ProductName =item.ProductCode+"~"+item.ProductName+"("+item.ProductSize+")"+item.UOMName;
            //        pudvm.Quantity = item.ProductQuantity;
            //        pudvm.UnitePrice = item.ProdutUnitePrice;
            //        pudvm.Discount = item.ProductDiscount;
            //        PurcheaseDetails.Add(pudvm);
            //    }
            //    purdvm.PurcheaseDetails = PurcheaseDetails;
            return products;
        }
        public IEnumerable<Purchase> GETbySearch(int? Id, string name, string code)
        {
            var result = _context.Purchases.Where(t => t.InvoiecNo == code && t.IsArchive == false).ToList();
            return result;
        }
        #endregion sigle method
        #region Save and Edit
       

        public string[] Save(PurchaseVM data)
        {
            string[] result = new string[6];
            try
            {
                var sql = @"exec [dbo].[SP_Purchease] @Option = {0}, @Id = {1}, @InvoiecNo = {2}, @SupplierId = {2},@EmployeeId = {3}, @Date = {4},
@CreatedBy = {5},@CreatedAt = {6},@CreatedFrom = {7}";
              
                    result[1] = _context.Database.ExecuteSqlCommand(sql, 1, data.Id, data.InvoiecNo, data.SupplierId, data.EmployeeId, data.Date, data.CreatedBy, data.CreatedAt, data.CreatedFrom).ToString();

                

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        public string[] Edit(PurchaseVM data)
        {
            string[] result = new string[6];
            try
            {
                var sql = @"exec [dbo].[SP_Purchease] @Option = {0}, @Id = {1}, @InvoiecNo = {2}, @SupplierId = {2},@EmployeeId = {3}, @Date = {4},
@CreatedBy = {5},@CreatedAt = {6},@CreatedFrom = {7}";
                    result[1] = _context.Database.ExecuteSqlCommand(sql, 2, data.Id, data.InvoiecNo, data.SupplierId, data.EmployeeId, data.Date, data.CreatedBy, data.CreatedAt, data.CreatedFrom).ToString();
            }
            catch (Exception ex)
            {

                throw ex;
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
        //public List<RPTVM> rptPurchease()
        //{
        //    List<RPTVM> vms = new List<RPTVM>();
        //    RPTVM vm = new RPTVM();
        //    OrganiazationDAL orgDal = new OrganiazationDAL();
        //    Organization org = new Organization();
        //    org = orgDal.GETAllOrganization().FirstOrDefault(); ;
        //    var pur = _context.SP_PurcheaseDetail(null,null,null,null,null,null).ToList();
        //    foreach (var pp in pur)
        //    {
        //        vm = new RPTVM();
        //        vm.Id = pp.Id;
        //        vm.InvoiecNo = pp.InvoiecNo;
        //        vm.Date = Convert.ToDateTime(pp.Date);
        //        vm.SupplierName = pp.SupplierName;
        //        vm.SupplierMobile = pp.SupplierMobile;
        //        vm.SupplierEmail = pp.SupplierEmail;
        //        vm.SupplierPresentAddress = pp.SupplierPresentAddress;
        //        vm.EmployeeId = pp.EmployeeId;
        //        vm.EmployeeCode = pp.EmployeeCode;
        //        vm.EmployeeName = pp.EmployeeName;
        //        vm.EmployeeMobile = pp.EmployeeMobile;
        //        vm.EmployeeEmail = pp.EmployeeEmail;
        //        vm.EmployeePresentAddress = pp.EmployeePresentAddress;
        //        vm.ProductId = pp.ProductId;
        //        vm.ProductCode = pp.ProductCode;
        //        vm.ProductName = pp.ProductName;
        //        vm.ProductDiscount = pp.ProductDiscount;
        //        vm.ProductQuantity = pp.ProductQuantity;
        //        vm.UOMName = pp.UOMName;
        //        vm.ProductSize = pp.ProductSize;
        //        vm.ProdutUnitePrice = pp.ProdutUnitePrice;
        //        vm.OrganizationId = org.Id;
        //        vm.OrganizationCode = org.Code;
        //        vm.OrganizationName = org.Name;
        //        vm.OrganizationMobile = org.Mobile;
        //        vm.OrganizationEmail = org.Email;
        //        vm.OrganizationPresentAddress = org.PresentAddress;

        //        vms.Add(vm);
        //    }
        //    return vms;
        //}
        public dynamic AutocompleteInvoice(string term)
        {
            var pro = _context.Purchases.Where(m => m.IsArchive == false && m.IsActive == true
                         && (m.InvoiecNo.Contains(term) || m.InvoiecNo.Contains(term))).OrderBy(m => m.InvoiecNo).Select(m => m.InvoiecNo).ToList();
            return pro;
        }
        #endregion Method
    }
}
