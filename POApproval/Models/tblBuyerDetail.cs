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
    
    public partial class tblBuyerDetail
    {
        public int intBuyerDetailCode { get; set; }
        public Nullable<int> intBuyerCode { get; set; }
        public Nullable<int> intUserCode { get; set; }
        public Nullable<System.DateTime> dtCreatedAt { get; set; }
        public Nullable<int> intCreatedByCode { get; set; }
        public Nullable<System.DateTime> dtModifyAt { get; set; }
        public Nullable<int> intModifyBy { get; set; }
    
        public virtual tblBuyer tblBuyer { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
