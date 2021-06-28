using POApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POApproval.ViewModel
{
   
    public class userDataViewModel
    {
        public IEnumerable<procSelectUserDetail_Result> getUserList { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string xpertLoginID { get; set; }
        public string usercode { get; set; }
        public string pwd { get; set; }
        public string status { get; set; }
        public string usergroup { get; set; }
        public bool bolIsApprovalLimit { get; set; }
        public bool bolIsNewUser { get; set; }
        public bool bolIsNewBuyer { get; set; }
        public bool bolIsManageBuyer { get; set; }
        public string SuperAdmin { get; set; }
    }
}