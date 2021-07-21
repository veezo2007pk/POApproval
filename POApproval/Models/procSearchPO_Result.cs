using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POApproval.Models
{
    public partial class procSearchPO_Result
    {
        public string strRejectReason { get; set; }
        public bool IsSelected { get; set; }
        public string criteria { get; set; }
        public int intPOCode { get; set; }
        public Nullable<long> PO_Number { get; set; }
        public string Supplier_Code { get; set; }
        public string Supplier_Name { get; set; }
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy")]

        public Nullable<System.DateTime> Creation_Date { get; set; }
        public string strPOStatus { get; set; }
        public string Buyer { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ApprovalLevel { get; set; }
        public string NextPOStatus { get; set; }
    }

}