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
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                List<procSearchPO_Result> lst = new List<procSearchPO_Result>();
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    if (reqCookies["SuperAdmin"].ToString() == "Y")
                    {
                        if (strPOStatus != null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = YEAR(GETDATE()) AND  tblPO.strPOStatus in (" + strPOStatus + ")", con);
                        }
                        else if (PONumber != null && strPOStatus == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE  PO_Number=" + PONumber + " and YEAR(Creation_Date)=YEAR(GETDATE())", con);

                        }
                        else if (strPOStatus != null && PONumber != null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = YEAR(GETDATE()) AND  tblPO.strPOStatus in (" + strPOStatus + ") and PO_Number=" + PONumber + "", con);

                        }
                        else if (strPOStatus == null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = YEAR(GETDATE()) AND  tblPO.strPOStatus ='Pending'", con);

                        }

                    }
                    else
                    {
                        if (strPOStatus != null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND tblPO.strPOStatus IN(" + strPOStatus + ") AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);
                        }
                        else if (PONumber != null && strPOStatus == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND PO_Number=" + PONumber + " AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);

                        }
                        else if (strPOStatus != null && PONumber != null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND PO_Number=" + PONumber + " and strPOStatus in (" + strPOStatus + ") AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);

                        }
                        else if (strPOStatus == null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND strPOStatus ='Pending' AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);

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
                        details.ApprovalLevel = rdr["ApprovalLevel"].ToString();
                        lst.Add(details);
                    }
                    return lst;
                }
            }
            else
            {
                List<procSearchPO_Result> lst = new List<procSearchPO_Result>();
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    if (HttpContext.Current.Session["SuperAdmin"].ToString() == "Y")
                    {
                        if (strPOStatus != null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = YEAR(GETDATE()) AND  tblPO.strPOStatus in (" + strPOStatus + ")", con);
                        }
                        else if (PONumber != null && strPOStatus == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE  PO_Number=" + PONumber + " and YEAR(Creation_Date)=YEAR(GETDATE())", con);

                        }
                        else if (strPOStatus != null && PONumber != null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = YEAR(GETDATE()) AND  tblPO.strPOStatus in (" + strPOStatus + ") and PO_Number=" + PONumber + "", con);

                        }
                        else if (strPOStatus == null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number,tblPO.Supplier_Code,tblPO.Supplier_Name,tblPO.Creation_Date,tblPO.strPOStatus,CASE WHEN tblPO.strPOStatus = 'Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus = 'Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus = 'Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus = 'Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus = 'Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount, 'Full Access' ApprovalLevel FROM tblPO WHERE YEAR(tblPO.Creation_Date) = YEAR(GETDATE()) AND  tblPO.strPOStatus ='Pending'", con);

                        }

                    }
                    else
                    {
                        if (strPOStatus != null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND tblPO.strPOStatus IN(" + strPOStatus + ") AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);
                        }
                        else if (PONumber != null && strPOStatus == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND PO_Number=" + PONumber + " AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);

                        }
                        else if (strPOStatus != null && PONumber != null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND PO_Number=" + PONumber + " and strPOStatus in (" + strPOStatus + ") AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);

                        }
                        else if (strPOStatus == null && PONumber == null)
                        {
                            com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, CASE WHEN tblPO.strPOStatus='Pending' THEN  'Reviewed 1' WHEN tblPO.strPOStatus='Reviewed 1' THEN  'Reviewed 2' WHEN tblPO.strPOStatus='Reviewed 2' THEN  'Reviewed 3' WHEN tblPO.strPOStatus='Reviewed 3' THEN  'Approved' WHEN tblPO.strPOStatus='Approved' THEN  'Approved' WHEN tblPO.strPOStatus='Rejected' THEN  'Rejected' END AS NextPOStatus, tblPO.Buyer,  tblPO.Qty,  tblPO.Amount , tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblManageApproval ON tblManageApproval.intBuyerCode=tblPO.staff_code INNER JOIN[SYSCOMDB].[dbo].system_user_login tblUser ON tblUser.usercode = tblManageApproval.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount  AND tblPO.Amount <= tblManageApproval.numToApprovalAmount  AND strPOStatus ='Pending' AND YEAR(tblPO.Creation_Date)=YEAR(GETDATE())", con);

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
                        details.ApprovalLevel = rdr["ApprovalLevel"].ToString();
                        lst.Add(details);
                    }
                    return lst;
                }
            }
         
        }
        public List<procRptPO_Result> GetPOReport(string intPOCode)
        {
            List<procRptPO_Result> lst = new List<procRptPO_Result>();
           procRptPO_Result rptPO = new procRptPO_Result();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procRptPO", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@intPOCode", intPOCode);
                com.Parameters.Add("@strUser", "dww");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    
                    if (rdr["intPOCode"] == DBNull.Value)
                    {
                        rptPO.intPOCode =0;
                    }

                    else
                    {
                        rptPO.intPOCode = Convert.ToInt32(rdr["intPOCode"]);
                    }

                    if (rdr["CurrentDate"] == DBNull.Value)
                    {
                        rptPO.CurrentDate = null;
                    }

                    else
                    {
                        rptPO.CurrentDate = Convert.ToDateTime(rdr["CurrentDate"]);
                    }

                    if (rdr["CurrentTime"] == DBNull.Value)
                    {
                        rptPO.CurrentTime = null;
                    }

                    else
                    {
                        rptPO.CurrentTime = rdr["CurrentTime"].ToString();
                    }

                    if (rdr["strPOStatus"] == DBNull.Value)
                    {
                        rptPO.strPOStatus = null;
                    }

                    else
                    {
                        rptPO.strPOStatus = rdr["strPOStatus"].ToString();
                    }
                    if (rdr["Supplier_Address"] == DBNull.Value)
                    {
                        rptPO.Supplier_Address = null;
                    }

                    else
                    {
                        rptPO.Supplier_Address = rdr["Supplier_Address"].ToString();
                    }
                    if (rdr["Brand_Name"] == DBNull.Value)
                    {
                        rptPO.Brand_Name = null;
                    }

                    else
                    {
                        rptPO.Brand_Name = rdr["Brand_Name"].ToString();
                    }

                    if (rdr["PO_Number"] == DBNull.Value)
                    {
                        rptPO.PO_Number = 0;
                    }

                    else
                    {
                        rptPO.PO_Number = Convert.ToInt64(rdr["PO_Number"]);
                    }

                    if (rdr["Shipto"] == DBNull.Value)
                    {
                        rptPO.Shipto = null;
                    }

                    else
                    {
                        rptPO.Shipto = rdr["Shipto"].ToString();
                    }

                    if (rdr["Supplier_Code"] == DBNull.Value)
                    {
                        rptPO.Supplier_Code = null;
                    }

                    else
                    {
                        rptPO.Supplier_Code = rdr["Supplier_Code"].ToString();
                    }

                    if (rdr["Supplier_Name"] == DBNull.Value)
                    {
                        rptPO.Supplier_Name = null;
                    }

                    else
                    {
                        rptPO.Supplier_Name =rdr["Supplier_Name"].ToString();
                    }

                    if (rdr["Store_Address"] == DBNull.Value)
                    {
                        rptPO.Store_Address = null;
                    }

                    else
                    {
                        rptPO.Store_Address = rdr["Store_Address"].ToString();
                    }

                    if (rdr["Creation_Date"] == DBNull.Value)
                    {
                        rptPO.Creation_Date = null;
                    }

                    else
                    {
                        rptPO.Creation_Date = Convert.ToDateTime(rdr["Creation_Date"]);
                    }

                    if (rdr["FOB"] == DBNull.Value)
                    {
                        rptPO.FOB = null;
                    }

                    else
                    {
                        rptPO.FOB = rdr["FOB"].ToString();
                    }

                    if (rdr["Buyer"] == DBNull.Value)
                    {
                        rptPO.Buyer =null;
                    }

                    else
                    {
                        rptPO.Buyer = rdr["Buyer"].ToString();
                    }

                    if (rdr["Delivery_Date"] == DBNull.Value)
                    {
                        rptPO.Delivery_Date = null;
                    }

                    else
                    {
                        rptPO.Delivery_Date = Convert.ToDateTime(rdr["Delivery_Date"]);
                    }

                    if (rdr["Valid_Date"] == DBNull.Value)
                    {
                        rptPO.Valid_Date = null;
                    }

                    else
                    {
                        rptPO.Valid_Date = Convert.ToDateTime(rdr["Valid_Date"]);
                    }

                    if (rdr["Shipment_Terms"] == DBNull.Value)
                    {
                        rptPO.Shipment_Terms = null;
                    }

                    else
                    {
                        rptPO.Shipment_Terms = rdr["Shipment_Terms"].ToString();
                    }

                    if (rdr["Payment_Term"] == DBNull.Value)
                    {
                        rptPO.Payment_Term = null;
                    }

                    else
                    {
                        rptPO.Payment_Term = rdr["Payment_Term"].ToString();
                    }

                    if (rdr["strRejectReason"] == DBNull.Value)
                    {
                        rptPO.strRejectReason =null;
                    }

                    else
                    {
                        rptPO.strRejectReason = rdr["strRejectReason"].ToString();
                    }

                    if (rdr["Contact_Person"] == DBNull.Value)
                    {
                        rptPO.Contact_Person = null;
                    }

                    else
                    {
                        rptPO.Contact_Person = rdr["Contact_Person"].ToString();
                    }

                    if (rdr["vendor_item_no"] == DBNull.Value)
                    {
                        rptPO.vendor_item_no = null;
                    }

                    else
                    {
                        rptPO.vendor_item_no = rdr["vendor_item_no"].ToString();
                    }

                    if (rdr["product_code"] == DBNull.Value)
                    {
                        rptPO.product_code = null;
                    }

                    else
                    {
                        rptPO.product_code = rdr["product_code"].ToString();
                    }

                    if (rdr["Description"] == DBNull.Value)
                    {
                        rptPO.Description = null;
                    }

                    else
                    {
                        rptPO.Description = rdr["Description"].ToString();
                    }

                    if (rdr["Qty"] == DBNull.Value)
                    {
                        rptPO.Qty = 0;
                    }

                    else
                    {
                        rptPO.Qty = Convert.ToInt32(rdr["Qty"]);
                    }

                    if (rdr["Sugg_Price"] == DBNull.Value)
                    {
                        rptPO.Sugg_Price = 0;
                    }

                    else
                    {
                        rptPO.Sugg_Price = Convert.ToDecimal(rdr["Sugg_Price"]);
                    }

                    if (rdr["Unit_Price"] == DBNull.Value)
                    {
                        rptPO.Unit_Price = 0;
                    }

                    else
                    {
                        rptPO.Unit_Price = Convert.ToDecimal(rdr["Unit_Price"]);
                    }

                    if (rdr["Foreign_Unit_Price"] == DBNull.Value)
                    {
                        rptPO.Foreign_Unit_Price = 0;
                    }

                    else
                    {
                        rptPO.Foreign_Unit_Price = Convert.ToDecimal(rdr["Foreign_Unit_Price"]);
                    }

                    if (rdr["Disc"] == DBNull.Value)
                    {
                        rptPO.Disc = 0;
                    }

                    else
                    {
                        rptPO.Disc = Convert.ToDecimal(rdr["Disc"]);
                    }

                    if (rdr["Amount"] == DBNull.Value)
                    {
                        rptPO.Amount = 0;
                    }

                    else
                    {
                        rptPO.Amount = Convert.ToDecimal(rdr["Amount"]);
                    }

                    if (rdr["Foreign_Amount"] == DBNull.Value)
                    {
                        rptPO.Foreign_Amount = 0;
                    }

                    else
                    {
                        rptPO.Foreign_Amount = Convert.ToDecimal(rdr["Foreign_Amount"]);
                    }


                    if (rdr["strUser"] == DBNull.Value)
                    {
                        rptPO.strUser = null;
                    }

                    else
                    {
                        rptPO.strUser = rdr["strUser"].ToString();
                    }

                    lst.Add(new procRptPO_Result
                    {
                        Supplier_Name = rptPO.Supplier_Name,
                        strPOStatus = rptPO.strPOStatus,
                        Supplier_Code = rptPO.Supplier_Code,
                        Buyer = rptPO.Buyer,
                        Creation_Date = rptPO.Creation_Date,

                        PO_Number = rptPO.PO_Number,
                        strRejectReason = rptPO.strRejectReason,
                        strUser = rptPO.strUser,
                        Delivery_Date = rptPO.Delivery_Date,
                        Shipment_Terms = rptPO.Shipment_Terms,
                        Shipto = rptPO.Shipto,
                        Store_Address = rptPO.Store_Address,
                        Sugg_Price = rptPO.Sugg_Price,
                        Description = rptPO.Description,
                        Amount = rptPO.Amount,
                        Contact_Person = rptPO.Contact_Person,
                        CurrentDate = rptPO.CurrentDate,
                        CurrentTime = rptPO.CurrentTime,
                        intPOCode = rptPO.intPOCode,
                        Disc = rptPO.Disc,
                        FOB = rptPO.FOB,
                        Foreign_Amount = rptPO.Foreign_Amount,
                        Valid_Date = rptPO.Valid_Date,
                        Foreign_Unit_Price = rptPO.Foreign_Unit_Price,
                        Payment_Term = rptPO.Payment_Term,
                        product_code = rptPO.product_code,
                        Qty = rptPO.Qty,
                        Unit_Price = rptPO.Unit_Price,
                        vendor_item_no = rptPO.vendor_item_no,
                        Brand_Name=rptPO.Brand_Name,
                        Supplier_Address = rptPO.Supplier_Address






                    });
                }
                return lst;
            }
        }
        public List<procSearchPO_Result> ListAll(int intUserCode, string strPOStatus, long PO_Number)
        {
            List<procSearchPO_Result> lst = new List<procSearchPO_Result>();
            procSearchPO_Result SearchPO = new procSearchPO_Result();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSearchPOMainPage", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@intUserCode", intUserCode);
                com.Parameters.Add("@PO_Number", PO_Number);
                com.Parameters.Add("@strPOStatus", "sdsd");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["Supplier_Name"] == DBNull.Value)
                    {
                        SearchPO.Supplier_Name = null;
                    }

                    else
                    {
                        SearchPO.Supplier_Name = rdr["Supplier_Name"].ToString();
                    }

                    if (rdr["strPOStatus"] == DBNull.Value)
                    {
                        SearchPO.strPOStatus = null;
                    }

                    else
                    {
                        SearchPO.strPOStatus = rdr["strPOStatus"].ToString();
                    }

                    if (rdr["Supplier_Code"] == DBNull.Value)
                    {
                        SearchPO.Supplier_Code = null;
                    }

                    else
                    {
                        SearchPO.Supplier_Code = rdr["Supplier_Code"].ToString();
                    }

                    if (rdr["Buyer"] == DBNull.Value)
                    {
                        SearchPO.Buyer = null;
                    }

                    else
                    {
                        SearchPO.Buyer = rdr["Buyer"].ToString();
                    }

                    if (rdr["Creation_Date"] == DBNull.Value)
                    {
                        SearchPO.Creation_Date = null;
                    }

                    else
                    {
                        SearchPO.Creation_Date = Convert.ToDateTime(rdr["Creation_Date"].ToString());
                    }

                    if (rdr["intPOCode"] == DBNull.Value)
                    {
                        SearchPO.intPOCode = 0;
                    }

                    else
                    {
                        SearchPO.intPOCode = Convert.ToInt32(rdr["intPOCode"].ToString());
                    }

                    if (rdr["PO_Number"] == DBNull.Value)
                    {
                        SearchPO.PO_Number = 0;
                    }

                    else
                    {
                        SearchPO.PO_Number = Convert.ToInt64(rdr["PO_Number"].ToString());
                    }

                    if (rdr["NextPOStatus"] == DBNull.Value)
                    {
                        SearchPO.NextPOStatus = null;
                    }

                    else
                    {
                        SearchPO.NextPOStatus = rdr["NextPOStatus"].ToString();
                    }

                    if (rdr["Amount"] == DBNull.Value)
                    {
                        SearchPO.Amount = null;
                    }

                    else
                    {
                        SearchPO.Amount = Convert.ToDecimal(rdr["Amount"].ToString());
                    }


                    if (rdr["Qty"] == DBNull.Value)
                    {
                        SearchPO.Qty = null;
                    }

                    else
                    {
                        SearchPO.Qty = Convert.ToInt32(rdr["Qty"].ToString());
                    }

                    if (rdr["ApprovalLevel"] == DBNull.Value)
                    {
                        SearchPO.ApprovalLevel = null;
                    }

                    else
                    {
                        SearchPO.ApprovalLevel = rdr["ApprovalLevel"].ToString();
                    }
                    lst.Add(new procSearchPO_Result
                    {
                        Supplier_Name = SearchPO.Supplier_Name,
                        strPOStatus = SearchPO.strPOStatus,
                        Supplier_Code = SearchPO.Supplier_Code,
                        Buyer = SearchPO.Buyer,
                        Creation_Date = SearchPO.Creation_Date,
                        intPOCode = SearchPO.intPOCode,
                        PO_Number = SearchPO.PO_Number,
                        NextPOStatus = SearchPO.NextPOStatus,
                        Amount = SearchPO.Amount,
                        Qty = SearchPO.Qty,
                        ApprovalLevel = SearchPO.ApprovalLevel






                    });
                }
                return lst;
            }
        }


        public List<procSearchPO_Result> ListAllprocSearch(string intUserCode, string pO_Number, string strPOStatus)
        {
            List<procSearchPO_Result> lst = new List<procSearchPO_Result>();
            procSearchPO_Result SearchPO = new procSearchPO_Result();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSearchPO", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@intUserCode", intUserCode);
                com.Parameters.Add("@PO_Number", pO_Number);
                com.Parameters.Add("@strPOStatus", strPOStatus);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["Supplier_Name"] == DBNull.Value)
                    {
                        SearchPO.Supplier_Name = null;
                    }

                    else
                    {
                        SearchPO.Supplier_Name = rdr["Supplier_Name"].ToString();
                    }

                    if (rdr["strPOStatus"] == DBNull.Value)
                    {
                        SearchPO.strPOStatus = null;
                    }

                    else
                    {
                        SearchPO.strPOStatus = rdr["strPOStatus"].ToString();
                    }

                    if (rdr["Supplier_Code"] == DBNull.Value)
                    {
                        SearchPO.Supplier_Code = null;
                    }

                    else
                    {
                        SearchPO.Supplier_Code = rdr["Supplier_Code"].ToString();
                    }

                    if (rdr["Buyer"] == DBNull.Value)
                    {
                        SearchPO.Buyer = null;
                    }

                    else
                    {
                        SearchPO.Buyer = rdr["Buyer"].ToString();
                    }

                    if (rdr["Creation_Date"] == DBNull.Value)
                    {
                        SearchPO.Creation_Date = null;
                    }

                    else
                    {
                        SearchPO.Creation_Date =Convert.ToDateTime( rdr["Creation_Date"].ToString());
                    }

                    if (rdr["intPOCode"] == DBNull.Value)
                    {
                        SearchPO.intPOCode = 0;
                    }

                    else
                    {
                        SearchPO.intPOCode = Convert.ToInt32(rdr["intPOCode"].ToString());
                    }

                    if (rdr["PO_Number"] == DBNull.Value)
                    {
                        SearchPO.PO_Number = 0;
                    }

                    else
                    {
                        SearchPO.PO_Number = Convert.ToInt64(rdr["PO_Number"].ToString());
                    }

                    if (rdr["NextPOStatus"] == DBNull.Value)
                    {
                        SearchPO.NextPOStatus =null;
                    }

                    else
                    {
                        SearchPO.NextPOStatus = rdr["NextPOStatus"].ToString();
                    }

                    if (rdr["Amount"] == DBNull.Value)
                    {
                        SearchPO.Amount = null;
                    }

                    else
                    {
                        SearchPO.Amount = Convert.ToDecimal( rdr["Amount"].ToString());
                    }


                    if (rdr["Qty"] == DBNull.Value)
                    {
                        SearchPO.Qty = null;
                    }

                    else
                    {
                        SearchPO.Qty = Convert.ToInt32(rdr["Qty"].ToString());
                    }

                    if (rdr["ApprovalLevel"] == DBNull.Value)
                    {
                        SearchPO.ApprovalLevel = null;
                    }

                    else
                    {
                        SearchPO.ApprovalLevel = rdr["ApprovalLevel"].ToString();
                    }
                    lst.Add(new procSearchPO_Result
                    {
                        Supplier_Name = SearchPO.Supplier_Name,
                        strPOStatus = SearchPO.strPOStatus,
                        Supplier_Code = SearchPO.Supplier_Code,
                        Buyer = SearchPO.Buyer,
                        Creation_Date = SearchPO.Creation_Date,
                        intPOCode = SearchPO.intPOCode,
                        PO_Number = SearchPO.PO_Number,
                        NextPOStatus= SearchPO.NextPOStatus,
                        Amount= SearchPO.Amount,
                        Qty= SearchPO.Qty,
                        ApprovalLevel= SearchPO.ApprovalLevel
                       





                    });
                }
                return lst;
            }
        }
        public int Add(int ID, string userCode, string status, string strRejectReason)
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