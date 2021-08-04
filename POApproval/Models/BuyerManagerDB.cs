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
    public class BuyerManagerDB
    {

        //Return list of all BuyerManagers  
        public List<procSelectBuyerManager_Result> ListAll()
        {
            List<procSelectBuyerManager_Result> lst = new List<procSelectBuyerManager_Result>();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSelectBuyerManager", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new procSelectBuyerManager_Result
                    {
                        intBuyerDetailCode= Convert.ToInt32(rdr["intBuyerDetailCode"]),

                        strBuyerName = rdr["strBuyerName"].ToString(),
                        logon_user_id= rdr["logon_user_id"].ToString(),
                    


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

                intCode = DAL.getNext_code("Select ISNULL(MAX(intBuyerDetailCode), 0) + 1 from tblBuyerDetail");



            }

            catch (Exception ex)

            {



                throw;

            }

            return intCode;



        }

        //Method for Adding an ManageAppproval  
        public int Add(tblBuyerDetail ManageAppproval)
        {
            int i;
            if (ManageAppproval.bolIsActive == null)
                ManageAppproval.bolIsActive = false;

            
            Int32 intManageAppprovalCodeMaxCode = (Int32)get_nextCode();
            if (intManageAppprovalCodeMaxCode == 0)
                intManageAppprovalCodeMaxCode = 1;
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            if(reqCookies != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("procInsertUpdateBuyerManager", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intBuyerDetailCode", ManageAppproval.intBuyerDetailCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);

                    com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intCreatedByCode", reqCookies["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyBy",reqCookies["intUserCode"].ToString());
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
                    SqlCommand com = new SqlCommand("procInsertUpdateBuyerManager", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intBuyerDetailCode", ManageAppproval.intBuyerDetailCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);

                    com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intCreatedByCode", HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyBy",HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@Action", "Insert");
                    i = com.ExecuteNonQuery();
                }
                return i;
            }
            
        }

        //Method for Updating ManageAppproval record  
        public int Update(tblBuyerDetail ManageAppproval)
        {
            if (ManageAppproval.bolIsActive == null )
                ManageAppproval.bolIsActive = false;
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            int i;
            if (reqCookies != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.cs))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("procInsertUpdateBuyerManager", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intBuyerDetailCode", ManageAppproval.intBuyerDetailCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);
                    com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intCreatedByCode", reqCookies["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyBy", reqCookies["intUserCode"].ToString());
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
                    SqlCommand com = new SqlCommand("procInsertUpdateBuyerManager", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@intBuyerDetailCode", ManageAppproval.intBuyerDetailCode);
                    com.Parameters.AddWithValue("@intUserCode", ManageAppproval.intUserCode);
                    com.Parameters.AddWithValue("@intBuyerCode", ManageAppproval.intBuyerCode);
                    com.Parameters.AddWithValue("@bolIsActive", ManageAppproval.bolIsActive);
                    com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intCreatedByCode", HttpContext.Current.Session["intUserCode"].ToString());
                    com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                    com.Parameters.AddWithValue("@intModifyBy", HttpContext.Current.Session["intUserCode"].ToString());
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
                SqlCommand com = new SqlCommand("procDeleteBuyerManager", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}