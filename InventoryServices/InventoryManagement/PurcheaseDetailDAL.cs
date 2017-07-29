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
  public  class PurcheaseDetailDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        public List<PurcheaseDetail> GetAll()
        {
            List<PurcheaseDetail> products = new List<PurcheaseDetail>();
            return products = _context.Database.SqlQuery<PurcheaseDetail>("exec [dbo].[SP_PurcheaseDetail] @Option = {0},@Id = {1} ", 3).ToList();
        }
        public PurcheaseDetail GetSigle(int Id)
        {
            PurcheaseDetail products = new PurcheaseDetail();
           return products = _context.Database.SqlQuery<PurcheaseDetail>("exec [dbo].[SP_PurcheaseDetail] @Option = {0},@Id = {1} ", 4, Id).Single();
        }
        public string[] SaveAndEdit1(PurcheaseDetailVM data)
        {
            string[] result = new string[6];
            try
            {
                try
                {
                    if (data == null) throw new ArgumentNullException("The expected data not found For Insert");
                    if (data.Purchasevm.Id == 0)
                    {
                        bool duplicateName = _context.Purchases.Any(m => m.IsArchive == false && m.InvoiecNo == data.Purchasevm.InvoiecNo);
                        if (duplicateName == true)
                        {
                            result[1] = "Your InvoiecNo is already Exit";
                            throw new ArgumentNullException("Your InvoiecNo is already Exit");
                        }
                        //data.Purchasevm.Id = GETAllPurchase.Count() + 111;
                        data.Purchasevm.IsActive = data.Purchasevm.IsActive == false ? false : true;
                        data.Purchasevm.Date = (Convert.ToDateTime(data.Purchasevm.Date).ToString("MM/dd/yy")).ToString();
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
                                          edit1.ProductId, edit1.Quantity);
                                }
                                var a = _context.PurcheaseDetails.Where(m => m.PurchaseId == data.Purchasevm.Id);
                                _context.PurcheaseDetails.RemoveRange(a);
                                _context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
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
                                        edit1.ProductId, data.Purchasevm.Id, data.Purchasevm.Date, edit1.Quantity, edit1.UnitePrice, true, edit1.Remarks, data.Purchasevm.CreatedAt
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
        public string[] SaveAndEdit(PurcheaseDetail data)
        {
            string[] result = new string[6];
            try
            {
                if(data ==null) throw new ArgumentNullException("The expected data not found For Insert");
                var sql = @"exec [dbo].[SP_PurcheaseDetail] @Option = {0}, @PurchaseId = {1}, @ProductId = {2}, @UnitePrice = {3},@Quantity = {4}, @Date = {5},@Discount ={5},@Slup={6},@Remarks={7}
,@CreatedAtBy = {8},@CreatedAt = {9},@CreatedFrom = {10},@Id={11}";
                if (data.Id > 0)
                {
                    result[1] = _context.Database.ExecuteSqlCommand(sql, 2, data.PurchaseId, data.ProductId, data.UnitePrice, data.Quantity, data.Date, data.Discount, data.Slup, data.Remarks,data.CreatedAtBy,data.CreatedAt,data.CreatedFrom,data.Id).ToString();

                }
                else
                {
                    result[1] = _context.Database.ExecuteSqlCommand(sql, 1, data.PurchaseId, data.ProductId, data.UnitePrice, data.Quantity, data.Date, data.Discount, data.Slup, data.Remarks, data.CreatedAtBy, data.CreatedAt, data.CreatedFrom, data.Id).ToString();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
