//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryViewModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Salutation_E { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string Mobile { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string PABX { get; set; }
        public string Email { get; set; }
        public string FAX { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsArchive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateFrom { get; set; }
    }
}
