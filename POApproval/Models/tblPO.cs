//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POApproval.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPO()
        {
            this.tblPODetails = new HashSet<tblPODetail>();
            this.tblPOHistories = new HashSet<tblPOHistory>();
        }
        public string[] strStatusName { get; set; }
        public int intPOCode { get; set; }
        public Nullable<long> PO_Number { get; set; }
        public string Store_Code { get; set; }
        public string Delivery_Location { get; set; }
        public Nullable<System.DateTime> Creation_Date { get; set; }
        public Nullable<System.DateTime> Delivery_Date { get; set; }
        public string Company_Name { get; set; }
        public string Ship_Store { get; set; }
        public string Store_Address { get; set; }
        public string Store_Tel { get; set; }
        public string Store_Fax { get; set; }
        public string Supplier_Code { get; set; }
        public string Supplier_Name { get; set; }
        public string Supplier_Address { get; set; }
        public string Contact_Person { get; set; }
        public Nullable<System.DateTime> po_print_date { get; set; }
        public string username { get; set; }
        public string PO_Status { get; set; }
        public string FOB { get; set; }
        public string Buyer { get; set; }
        public Nullable<System.DateTime> Valid_Date { get; set; }
        public string Shipment_Terms { get; set; }
        public string Payment_Term { get; set; }
        public string approved { get; set; }
        public string approved_by { get; set; }
        public Nullable<System.DateTime> Approved_Date { get; set; }
        public string Cancelled { get; set; }
        public string Cancelled_By { get; set; }
        public Nullable<System.DateTime> Cancelled_date { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string strPOStatus { get; set; }
        public Nullable<System.DateTime> dtApproved { get; set; }
        public string strRejectReason { get; set; }
        public Nullable<bool> bolIsRejectedPOEmailSent { get; set; }
        public string staff_code { get; set; }
        public string brand_name { get; set; }
        public string supplier_email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPODetail> tblPODetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPOHistory> tblPOHistories { get; set; }
    }
}
