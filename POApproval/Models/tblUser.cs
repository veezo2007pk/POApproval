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
    
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            this.tblManageApprovals = new HashSet<tblManageApproval>();
            this.tblPOHistories = new HashSet<tblPOHistory>();
            this.tblBuyerDetails = new HashSet<tblBuyerDetail>();
        }
        public bool RememberMe { get; set; }
        public int intUserCode { get; set; }
        public string logon_user_id { get; set; }
        public string UserPassword { get; set; }
        public string staff_code { get; set; }
        public string staff_name { get; set; }
        public string logon_user_name { get; set; }
        public string staff_type { get; set; }
        public string user_group { get; set; }
        public string staff_grade { get; set; }
        public string staff_category { get; set; }
        public string remark { get; set; }
        public string strDepartmentName { get; set; }
        public Nullable<System.DateTime> date_join { get; set; }
        public Nullable<System.DateTime> date_confirm { get; set; }
        public string date_resign { get; set; }
        public string staff_id { get; set; }
        public string issue_store_code { get; set; }
        public string email { get; set; }
        public Nullable<bool> bolIsApprovalLimit { get; set; }
        public Nullable<bool> bolIsNewUser { get; set; }
        public Nullable<bool> bolIsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblManageApproval> tblManageApprovals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPOHistory> tblPOHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBuyerDetail> tblBuyerDetails { get; set; }
    }
}
