using InventoryServices.Config;
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
   public class SalesDAL
    {
       #region Declare
       InventoryEntities _context = new InventoryEntities();
       #endregion Declare
       #region SelectAll
       public IEnumerable<Sale> GETAllSale { get { return _context.Sales.Where(m => m.IsArchive == false).AsEnumerable(); } }
       public List<SalesVM> GETAllSales()
       {
           List<SalesVM> SalesVM = new List<SalesVM>();
           SalesVM = _context.Database.SqlQuery<SalesVM>("exec [dbo].[sp_SalesDetails]").ToList();
           return SalesVM;
       }
       public SalesVM GetSigle(int Id)
       {
           SalesVM salevm = new SalesVM();
           salevm.Sales = _context.Sales.FirstOrDefault(m => m.Id == Id);
           salevm.SalesDetailvms = _context.SalesDetails.Where(m => m.SalesId == Id).ToList();
           return salevm;
       }
       #endregion SelectAll
       #region Save and Edit
       public string[] SaveAndEdit(SalesVM data)
       {
           StockDAL _dal = new StockDAL();
           StockVM stock = new StockVM();
           string[] result = new string[6];
           try
           {
               try
               {
                   if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
                   if (data.Sales.Id == 0)
                   {
                       data.Sales.Id = _context.Sales.Count()+111 ;
                       data.Sales.IsActive = data.Sales.IsActive == false ? false : true;
                       //data.Date = (Convert.ToDateTime(data.Date).ToString("MM/dd/yy")).ToString();
                       data.Sales.IsArchive = false;
                       data.Sales.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                       data.Sales.CreatedAt = DateTime.Now.ToString("MM/dd/yy");
                       data.Sales.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                       _context.Sales.Add(data.Sales);
                       SalesDetail purvms = new SalesDetail();
                       if (data.SalesDetailvms.Count() > 0 && data.SalesDetailvms != null)
                       {
                           foreach (var purdetail in data.SalesDetailvms)
                           {
                               purvms.ProductId = purdetail.ProductId;
                               purdetail.SalesId = data.Sales.Id;
                               purdetail.CreatedAt = data.Sales.CreatedAt;
                               purdetail.CreatedBy = data.Sales.CreatedBy;
                               purdetail.CreatedFrom = data.Sales.CreatedFrom;
                               _context.SalesDetails.Add(purdetail);
                               _context.SaveChanges();
                               stock.StockStutes = false;
                               stock.ProductId = purdetail.ProductId;
                               stock.TotalQuantity = purdetail.SalesQuantity;
                               stock.TotalDiscount = purdetail.Discount;
                               stock.TotalReplace = purdetail.Replace;
                               stock.TotalReturn = purdetail.Return;
                               stock.TotalPrice = purdetail.SalesQuantity * purdetail.UnitePrice;
                               _dal.SaveAndEditUpdate(stock);
                           }
                       }
                       result[1] = "Sales Data Save";
                       result[0] = "Successfully";
                   }
                   else
                   {
                       var edit = _context.Sales.Find(data.Sales.Id);
                       if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                       //data.Date = (Convert.ToDateTime(data.Date).ToString("MM/dd/yy")).ToString();
                       data.Sales.IsActive = data.Sales.IsActive == false ? false : true;
                       data.Sales.IsArchive = false;
                       data.Sales.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                       data.Sales.LastUpdateAt = DateTime.Now.ToString("MM/dd/yy");
                       data.Sales.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                       _context.Entry(edit).CurrentValues.SetValues(data.Sales);
                       result[1] = "Sales Data Update";
                       result[0] = "Successfully";
                       _context.SaveChanges();
                       if (data.SalesDetailvms.Count() > 0 && data.SalesDetailvms != null)
                       {
                           try
                           {
                               var a = _context.SalesDetails.Where(m => m.SalesId == data.Id);
                               _context.SalesDetails.RemoveRange(a);
                               _context.SaveChanges();
                           }
                           catch (Exception ex)
                           {
                               throw new ArgumentNullException("the not delete");
                           }
                           foreach (var purdetail in data.SalesDetailvms)
                           {
                               purdetail.ProductId = purdetail.ProductId;
                               purdetail.SalesId = data.Sales.Id;
                               purdetail.CreatedAt = data.Sales.CreatedAt;
                               purdetail.CreatedBy = data.Sales.CreatedBy;
                               purdetail.CreatedFrom = data.Sales.CreatedFrom;
                               _context.SalesDetails.Add(purdetail);
                               _context.SaveChanges();
                               stock.StockStutes = false;
                               stock.ProductId = purdetail.ProductId;
                              
                               stock.TotalQuantity = purdetail.SalesQuantity;
                               stock.TotalDiscount = purdetail.Discount;
                               stock.TotalReplace = purdetail.Replace;
                               stock.TotalReturn = purdetail.Return;
                               stock.TotalPrice = purdetail.SalesQuantity * purdetail.UnitePrice;
                               _dal.SaveAndEditUpdate(stock);
                           }
                       }
                   }
               }
               catch (Exception ex)
               {
                   result[1] = ex.Message;
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
        #region report
       public List<RPTVM> rptSales()
       {
           List<RPTVM> vms = new List<RPTVM>();
           RPTVM vm = new RPTVM();
           OrganiazationDAL orgDal = new OrganiazationDAL();
           Organization org = new Organization();
           org = orgDal.GETAllOrganization().FirstOrDefault(); ;
           var pur = _context.sp_salesDatailrpts().ToList();
           foreach (var pp in pur)
           {
               vm = new RPTVM();
vm.Id=pp.Id;
vm.Datetimes = Convert.ToDateTime(pp.Date);
vm.Discount=pp.Discount;
vm.EventName=pp.EventName;
vm.EventAamount=pp.EventAamount;
vm.InvoiecNo=pp.InvoiecNo;
vm.PackQuantity=pp.PackQuantity;
vm.PackUnitPrice=pp.PackUnitPrice;
vm.GrandTotal=pp.GrandTotal;
vm.TotalDiscount=pp.TotalDiscount;
vm.TotalPaid=pp.TotalPaid;
vm.TotalPrice=pp.TotalPrice;
vm.TotalReplace=pp.TotalReplace;
vm.TotalReturn=pp.TotalReturn;
vm.TotalSlup=pp.TotalSlup;
vm.ZoneId=pp.ZoneId;
vm.ZoneName=pp.ZoneName;
vm.MarketId=pp.MarketId;
vm.MarketName=pp.MarketName;
vm.ProductId=pp.ProductId;
vm.ProductCode=pp.ProductCode;
vm.ProductName=pp.ProductName;
vm.ProDiscount=pp.ProDiscount;
vm.ProReceiveQuantity=pp.AssaignQuantity;
vm.ProSalesQuantity=pp.SalesQuantity;
vm.ProUnitePrice=pp.ProUnitePrice;
vm.ProDatetime=Convert.ToDateTime(pp.ProDatetime);
vm.ProRetrun=pp.ProRetrun;
vm.ProReplace=pp.ProReplace;
vm.ProSlup=pp.ProSlup;
vm.UOMName=pp.UOMName;
vm.ProductSize= pp.ProductSize;
vm.EmployeeId=pp.EmployeeId;
vm.EmployeeCode=pp.EmployeeCode;
vm.EmployeeName=pp.EmployeeName;
vm.EmployeeMobile=pp.EmployeeMobile;
vm.EmployeeEmail=pp.EmployeeEmail;
vm.EmployeePresentAddress=pp.EmployeePresentAddress;
vm.CustomerId=pp.CustomerId;
vm.CustomerCode=pp.CustomerCode;
vm.CustomerName=pp.CustomerName;
vm.CustomerMobile=pp.CustomerMobile;
vm.CustomerEmail=pp.CustomerEmail;
vm.CustomerPresentAddress = pp.CustomerPresentAddress;
vm.OrganizationId=org.Id;
vm.OrganizationCode=org.Code;
vm.OrganizationName=org.Name;
vm.OrganizationMobile=org.Mobile;
vm.OrganizationEmail=org.Email;
vm.OrganizationPresentAddress=org.PresentAddress;
               vms.Add(vm);
           }
           return vms;
       }
        #endregion report
    }
}
