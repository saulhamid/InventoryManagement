using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryViewModel.ViewModel
{
   public class RPTVM
    {
        public int? Id { get; set; }

        public string InvoiecNo { get; set; }
        public DateTime Date { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierMobile { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPresentAddress { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeMobile { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePresentAddress { get; set; }
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductDiscount { get; set; }
        public decimal? ProductQuantity { get; set; }
        public decimal? ProductUnitePrice { get; set; }
        public decimal? ProdutUnitePrice { get; set; }
        public decimal? TotalDiscount { get; set; }
        public int? OrganizationId { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationMobile { get; set; }
        public string OrganizationEmail { get; set; }
        public string OrganizationPresentAddress { get; set; }

        public decimal? Discount { get; set; }
        public string EventName { get; set; }
        public decimal? EventAamount { get; set; }
        public decimal? PackQuantity { get; set; }
        public decimal? PackUnitPrice { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalReplace { get; set; }
        public decimal? TotalReturn { get; set; }
        public decimal? TotalSlup { get; set; }

        public int? ZoneId { get; set; }

        public string ZoneName { get; set; }

        public int? MarketId { get; set; }

        public string MarketName { get; set; }

        public decimal? ProDiscount { get; set; }

        public decimal? ProReceiveQuantity { get; set; }

        public decimal? ProSalesQuantity { get; set; }
        public DateTime ProDatetime { get; set; }
        public decimal? ProUnitePrice { get; set; }
        public decimal? ProRetrun { get; set; }
        public decimal? ProReplace { get; set; }
        public decimal? ProSlup { get; set; }
        public decimal? Total { get; set; }

        public string UOMName { get; set; }

        public string ProductSize { get; set; }

        public int? CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string CustomerMobile { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPresentAddress { get; set; }



        public DateTime Datetimes { get; set; }
    }
}
