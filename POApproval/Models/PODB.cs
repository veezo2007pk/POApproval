using POApproval.GlobalInfo;
using POApproval.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POApproval.Models
{
    public class PODB
    {
        SqlCommand com;
        public List<procSearchPO_Result> SearchPO(int intUserCode, string strPOStatus, long? PONumber)
        {
            List<procSearchPO_Result> lst = new List<procSearchPO_Result>();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                if (HttpContext.Current.Session["SuperAdmin"].ToString() == "Y")
                {
                    if (strPOStatus != null && PONumber == null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = '2021' AND  tblPO.strPOStatus in (" + strPOStatus + ")", con);
                    }
                    else if (PONumber != null && strPOStatus == null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE  PO_Number=" + PONumber + " and YEAR(Creation_Date)='2021'", con);

                    }
                    else if (strPOStatus != null && PONumber != null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = '2021' AND  tblPO.strPOStatus in (" + strPOStatus + ") and PO_Number=" + PONumber + "", con);

                    }
                    else if (strPOStatus == null && PONumber == null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = '2021' AND  tblPO.strPOStatus ='Pending'", con);

                    }

                }
                else
                {
                    if (strPOStatus != null && PONumber == null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode, tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approve' WHEN tblPO.strPOStatus = 'Approve' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus = 'Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN[BI_Staging].[dbo].system_user_login tblBuyer ON tblBuyer.fullname = tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.usercode INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.usercode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode ="+intUserCode+" AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount AND tblPO.Amount <= tblManageApproval.numToApprovalAmount and  strPOStatus in (" + strPOStatus + ") and YEAR(Creation_Date)='2021'", con);
                    }
                    else if (PONumber != null && strPOStatus == null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode, tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approve' WHEN tblPO.strPOStatus = 'Approve' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus = 'Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN[BI_Staging].[dbo].system_user_login tblBuyer ON tblBuyer.fullname = tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.usercode INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.usercode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode =" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount AND tblPO.Amount <= tblManageApproval.numToApprovalAmount and PO_Number=" + PONumber + " and YEAR(Creation_Date)='2021'", con);

                    }
                    else if (strPOStatus != null && PONumber != null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode, tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approve' WHEN tblPO.strPOStatus = 'Approve' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus = 'Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN[BI_Staging].[dbo].system_user_login tblBuyer ON tblBuyer.fullname = tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.usercode INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.usercode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode =" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount AND tblPO.Amount <= tblManageApproval.numToApprovalAmount and  PO_Number=" + PONumber + " and strPOStatus in (" + strPOStatus + ") and YEAR(Creation_Date)='2021'", con);

                    }
                    else if (strPOStatus == null && PONumber == null)
                    {
                        com = new SqlCommand("SELECT tblPO.intPOCode, tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approve' WHEN tblPO.strPOStatus = 'Approve' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus = 'Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN[BI_Staging].[dbo].system_user_login tblBuyer ON tblBuyer.fullname = tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.usercode INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.usercode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode =" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount AND tblPO.Amount <= tblManageApproval.numToApprovalAmount and   strPOStatus ='Pending' and YEAR(Creation_Date)='2021'", con);

                    }
                }
                //com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.Add("@PO_Number", PO_Number);
                //com.Parameters.Add("@strPOStatus", strPOStatus);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var details = new procSearchPO_Result();
                    details.Supplier_Name = rdr["Supplier_Name"].ToString();
                    details.strPOStatus = rdr["strPOStatus"].ToString();
                    details.Supplier_Code = rdr["Supplier_Code"].ToString();
                    details.Buyer = rdr["Buyer"].ToString();
                    details.Creation_Date = Convert.ToDateTime(rdr["Creation_Date"].ToString());
                    details.intPOCode = Convert.ToInt32(rdr["intPOCode"].ToString());
                    details.PO_Number = Convert.ToInt64(rdr["PO_Number"].ToString());
                    details.Qty = Convert.ToInt32(rdr["Qty"].ToString());
                    details.NextPOStatus = rdr["NextPOStatus"].ToString();
                    details.Amount = Convert.ToDecimal(rdr["Amount"].ToString());
                    details.ApprovalLevel =rdr["ApprovalLevel"].ToString();
                    lst.Add(details);
                }
                return lst;
            }
        }

        public List<procSearchPO_Result> ListAll(string strPOStatus, long PO_Number)
        {
            List<procSearchPO_Result> lst = new List<procSearchPO_Result>();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSearchPO", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@PO_Number", PO_Number);
                com.Parameters.Add("@strPOStatus", strPOStatus);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new procSearchPO_Result
                    {
                        Supplier_Name= rdr["Supplier_Name"].ToString(),
                        strPOStatus= rdr["strPOStatus"].ToString(),
                        Supplier_Code= rdr["Supplier_Code"].ToString(),
                        Buyer= rdr["Buyer"].ToString(),
                        Creation_Date=Convert.ToDateTime( rdr["Creation_Date"].ToString()),
                        intPOCode=Convert.ToInt32( rdr["intPOCode"].ToString()),
                        PO_Number= Convert.ToInt64(rdr["PO_Number"].ToString())

                     


                    });
                }
                return lst;
            }
        }
        public int Add(int ID, int userCode, string status, string strRejectReason)
        {
            int i;
            SqlConnection con = new SqlConnection(ConnectionString.cs);
            SqlTransaction Tran;
            //using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            //{

            con.Open();
           // Tran = con.BeginTransaction();
                try
                {
                    SqlCommand com = new SqlCommand("procInsertUpdatePOHistory", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intPOCode", ID);
                    com.Parameters.AddWithValue("@strPOStatus", status);
                    com.Parameters.AddWithValue("@intUserCode", userCode);
                if (strRejectReason != null)
                {
                    com.Parameters.AddWithValue("@strRejectReason", strRejectReason);
                }
                else
                {
                    com.Parameters.AddWithValue("@strRejectReason", DBNull.Value);
                }
                com.Parameters.AddWithValue("@Action", "Insert");
                    i = com.ExecuteNonQuery();
                   // Tran.Commit();
                }
                catch (Exception ex)
                {
                    //Tran.Rollback();
                    throw ex;
                }
               
            //}
            return i;
        }

    }
}