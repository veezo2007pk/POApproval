using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POApproval.Helper
{
    public class UserViewModel
    {
        public Nullable<bool> bolIsNewBuyer { get; set; }
        public Nullable<bool> bolIsManageBuyer { get; set; }
        public Nullable<bool> SuperAdmin { get; set; }
        public int intUserCode { get; set; }
        public string fullname { get; set; }
        public string usermenustring { get; set; }
        public string pwd { get; set; }
        public List<int?> usermenuids { get; set; }
        public string staff_code { get; set; }
        public string staff_name { get; set; }
        public string logon_user_name { get; set; }
        public string staff_type { get; set; }
        public string user_group { get; set; }
        public string staff_grade { get; set; }
        public string staff_category { get; set; }
        public string remark { get; set; }
        public string usergroup { get; set; }
        public Nullable<System.DateTime> date_join { get; set; }
        public Nullable<System.DateTime> date_confirm { get; set; }
        public string date_resign { get; set; }
        public string staff_id { get; set; }
        public string usercode { get; set; }
        public string xpertLoginID { get; set; }
        public string issue_store_code { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public Nullable<bool> bolIsApprovalLimit { get; set; }
        public Nullable<bool> bolIsNewUser { get; set; }
       
        public Nullable<bool> bolIsActive { get; set; }
    }
    public class BuyerViewModel
    {
        public int intBuyerCode { get; set; }
        public string strBuyerName { get; set; }
        public Nullable<bool> bolIsActive { get; set; }
        public Nullable<System.DateTime> dtCreatedAt { get; set; }
        public Nullable<int> intCreatedByCode { get; set; }
        public Nullable<System.DateTime> dtModifyAt { get; set; }
        public Nullable<int> intModifyBy { get; set; }
    }
    public class BuyerManagerViewModel
    {
        public int intBuyerDetailCode { get; set; }
        public Nullable<int> intBuyerCode { get; set; }
        public Nullable<int> intUserCode { get; set; }
        public Nullable<System.DateTime> dtCreatedAt { get; set; }
        public Nullable<int> intCreatedByCode { get; set; }
        public Nullable<System.DateTime> dtModifyAt { get; set; }
        public Nullable<int> intModifyBy { get; set; }

    }
}