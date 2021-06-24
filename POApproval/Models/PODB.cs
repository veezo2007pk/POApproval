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
                
                if (strPOStatus != null && PONumber==null) {
                    com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount ,tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblBuyer ON tblBuyer.strBuyerName=tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.intBuyerCode INNER JOIN tblUser ON tblUser.intUserCode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode=" + intUserCode+" AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount and tblPO.Amount <= tblManageApproval.numToApprovalAmount and  strPOStatus in (" + strPOStatus+ ") and YEAR(Creation_Date)='2021'", con);
                }
                else if (PONumber != null && strPOStatus==null)
                {
                    com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount ,tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblBuyer ON tblBuyer.strBuyerName=tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.intBuyerCode INNER JOIN tblUser ON tblUser.intUserCode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount and tblPO.Amount <= tblManageApproval.numToApprovalAmount and PO_Number="+PONumber+ " and YEAR(Creation_Date)='2021'", con);

                }
                else if(strPOStatus != null && PONumber != null)
                {
                    com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount ,tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblBuyer ON tblBuyer.strBuyerName=tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.intBuyerCode INNER JOIN tblUser ON tblUser.intUserCode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount and tblPO.Amount <= tblManageApproval.numToApprovalAmount and  PO_Number=" + PONumber+ " and strPOStatus in (" + strPOStatus + ") and YEAR(Creation_Date)='2021'", con);

                }
                else if(strPOStatus == null && PONumber == null)
                {
                    com = new SqlCommand("SELECT tblPO.intPOCode,tblPO.PO_Number, tblPO.Supplier_Code, tblPO.Supplier_Name, tblPO.Creation_Date, tblPO.strPOStatus, tblPO.Buyer, tblPO.Qty, tblPO.Amount ,tblApprovalLevel.strApprovalLevelName ApprovalLevel FROM tblPO INNER JOIN tblBuyer ON tblBuyer.strBuyerName=tblPO.Buyer INNER JOIN tblBuyerDetail ON tblBuyerDetail.intBuyerCode = tblBuyer.intBuyerCode INNER JOIN tblUser ON tblUser.intUserCode = tblBuyerDetail.intUserCode INNER JOIN tblManageApproval ON tblManageApproval.intUserCode = tblUser.intUserCode INNER JOIN  tblApprovalLevel ON tblApprovalLevel.intApprovalLevelCode = tblManageApproval.intApprovalLevelCode  WHERE tblManageApproval.intUserCode=" + intUserCode + " AND   tblPO.Amount > tblManageApproval.numFromApprovalAmount and tblPO.Amount <= tblManageApproval.numToApprovalAmount and   strPOStatus ='Pending' and YEAR(Creation_Date)='2021'", con);

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