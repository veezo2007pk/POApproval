using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POApproval.Models
{
    public partial class procRptPO_Result
    {
        public int intPOCode { get; set; }
        public Nullable<System.DateTime> CurrentDate { get; set; }
        public string CurrentTime { get; set; }
        public string strPOStatus { get; set; }
        public Nullable<long> PO_Number { get; set; }
        public string Shipto { get; set; }
        public string Supplier_Code { get; set; }
        public string Supplier_Name { get; set; }
        public string Store_Address { get; set; }
        public Nullable<System.DateTime> Creation_Date { get; set; }
        public string FOB { get; set; }
        public string Buyer { get; set; }
        public Nullable<System.DateTime> Delivery_Date { get; set; }
        public Nullable<System.DateTime> Valid_Date { get; set; }
        public string Shipment_Terms { get; set; }
        public string Payment_Term { get; set; }
        public string strRejectReason { get; set; }
        public string Contact_Person { get; set; }
        public string vendor_item_no { get; set; }
        public string product_code { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Sugg_Price { get; set; }
        public Nullable<decimal> Unit_Price { get; set; }
        public Nullable<decimal> Foreign_Unit_Price { get; set; }
        public Nullable<decimal> Disc { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Foreign_Amount { get; set; }
        public string strUser { get; set; }
    }
}