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
    
    public partial class tblPOHistory
    {
        public int intPOHistoryCode { get; set; }
        public Nullable<int> intPOCode { get; set; }
        public string strPOStatus { get; set; }
        public string intUserCode { get; set; }
        public Nullable<System.DateTime> dtCreatedAt { get; set; }
    
        public virtual tblPO tblPO { get; set; }
    }
}
