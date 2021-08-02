using POApproval.GlobalInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace POApproval.Models
{
    public class ManageApprovalDB
    {

        //Return list of all ManageApprovals  
        public List<procSelectManageApproval_Result> ListAll()
        {
            List<procSelectManageApproval_Result> lst = new List<procSelectManageApproval_Result>();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSelectManageApproval", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new procSelectManageApproval_Result
                    {
                        intManageApprovalCode= Convert.ToInt32(rdr["intManageApprovalCode"]),
                        UserCode = rdr["UserCode"].ToString(),                   
                        Department = rdr["Department"].ToString(),
                        ApprovalAmount= rdr["ApprovalAmount"].ToString(),
                        ApprovalLevel= rdr["ApprovalLevel"].ToString(),
                        ApprovalName= rdr["ApprovalName"].ToString(),
                        Status = rdr["Status"].ToString(),


                    });
                }
                return lst;
            }
        }
        private int get_nextCode()

        {

            int intCode;

            try

            {

                intCode = DAL.getNext_code("Select ISNULL(MAX(intManageApprovalCode), 0) + 1 from tblManageApproval");



            }

            catch (Exception ex)

            {



                throw;

            }

            return intCode;



        }

        //Method for Adding an ManageAppproval  
        public int Add(tblManageApproval ManageAppproval)
        {
            int i;
          
            if (ManageAppproval.bolIsActive == null)
                ManageAppproval.bolIsActive = false;

            Int32 intManageAppprovalCodeMaxCode = (Int32)get_nextCode();
            if (intManageAppprovalCodeMaxCode == 0)
                intManageAppprovalCodeMaxCode = 1;
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("procInsertUpdateManageApproval", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intManageApprovalCode", ManageAppproval.intManageApprovalCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intApprovalLevelCode", ManageAppproval.intApprovalLevelCode);
                    com.Parameters.AddWithValue("@numFromApprovalAmount", ManageAppproval.numFromApprovalAmount);
                    com.Parameters.AddWithValue("@numToApprovalAmount", ManageAppproval.numToApprovalAmount);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);
                    com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intCreatedByCode", reqCookies["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyByCode", reqCookies["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@Action", "Insert");
                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("procInsertUpdateManageApproval", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intManageApprovalCode", ManageAppproval.intManageApprovalCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intApprovalLevelCode", ManageAppproval.intApprovalLevelCode);
                    com.Parameters.AddWithValue("@numFromApprovalAmount", ManageAppproval.numFromApprovalAmount);
                    com.Parameters.AddWithValue("@numToApprovalAmount", ManageAppproval.numToApprovalAmount);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);
                    com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intCreatedByCode", (HttpContext.Current.Session["intUserCode"].ToString()));
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyByCode", HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@Action", "Insert");
                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            
        }

        //Method for Updating ManageAppproval record  
        public int Update(tblManageApproval ManageAppproval)
        {
            int i;
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("procInsertUpdateManageApproval", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intManageApprovalCode", ManageAppproval.intManageApprovalCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@intApprovalLevelCode", ManageAppproval.intApprovalLevelCode);
                    com.Parameters.AddWithValue("@numFromApprovalAmount", ManageAppproval.numFromApprovalAmount);
                    com.Parameters.AddWithValue("@numToApprovalAmount", ManageAppproval.numToApprovalAmount);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);
                    com.Parameters.AddWithValue("@dtCreatedAt", ManageAppproval.dtCreatedAt);
                    com.Parameters.AddWithValue("@intCreatedByCode", HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyByCode", reqCookies["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@Action", "Update");
                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("procInsertUpdateManageApproval", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intManageApprovalCode", ManageAppproval.intManageApprovalCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@intApprovalLevelCode", ManageAppproval.intApprovalLevelCode);
                    com.Parameters.AddWithValue("@numFromApprovalAmount", ManageAppproval.numFromApprovalAmount);
                    com.Parameters.AddWithValue("@numToApprovalAmount", ManageAppproval.numToApprovalAmount);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);
                    com.Parameters.AddWithValue("@dtCreatedAt", ManageAppproval.dtCreatedAt);
                    com.Parameters.AddWithValue("@intCreatedByCode", HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyByCode", HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@Action", "Update");
                    i = com.ExecuteNonQuery();
                }
                return i;
            }
           
        }

        //Method for Deleting an ManageAppproval  
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procDeleteManageApproval", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}