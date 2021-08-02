using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POApproval.Models
{
    public class procCheckApprovalLevelSequence
    {
        public Nullable<long> PO_Number { get; set; }
        public string ApprovalLevel { get; set; }
    }
}