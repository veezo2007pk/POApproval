using POApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POApproval.ViewModel
{
    public class POViewModel
    {
        public tblPO tblPO { get; set; }
        public List<tblPODetail> tblPODetails { get; set; }
        public List<procGetUserApprovalLog_Result> tblPOHistories { get; set; }
        public List<tblManageApproval> tblManageApprovals { get; set; }
    }
}