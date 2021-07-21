using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POApproval.Helper
{
    public class ManageApprovalViewModel
    {
        public string intBuyerCode { get; set; }
        public int intManageApprovalCode { get; set; }
        public string intUserCode { get; set; }
        public int intApprovalLevelCode { get; set; }
        public Nullable<decimal> numFromApprovalAmount { get; set; }
        public Nullable<decimal> numToApprovalAmount { get; set; }
        public Nullable<bool> bolIsActive { get; set; }
        public Nullable<System.DateTime> dtCreatedAt { get; set; }
        public Nullable<int> intCreatedByCode { get; set; }
        public Nullable<System.DateTime> dtModifyAt { get; set; }
        public Nullable<int> intModifyBy { get; set; }
    }
}