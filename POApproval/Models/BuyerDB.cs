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
    public class BuyerDB
    {

        //Return list of all Buyers  
        public List<procSelectBuyer_Result> ListAll()
        {
            List<procSelectBuyer_Result> lst = new List<procSelectBuyer_Result>();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSelectBuyer", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new procSelectBuyer_Result
                    {

                        BuyeCode = Convert.ToInt32(rdr["BuyeCode"]),
                        BuyerName = rdr["BuyerName"].ToString(),
                      
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

                intCode = DAL.getNext_code("Select ISNULL(MAX(intBuyerCode), 0) + 1 from tblBuyer");



            }

            catch (Exception ex)

            {



                throw;

            }

            return intCode;



        }

        //Method for Adding an Buyer  
        public int Add(tblBuyer Buyer)
        {

            int i;
           
            if (Buyer.bolIsActive == null)
                Buyer.bolIsActive = false;

            Int32 intBuyerCodeMaxCode = (Int32)get_nextCode();
            if (intBuyerCodeMaxCode == 0)
                intBuyerCodeMaxCode = 1;


            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procInsertUpdateBuyer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@intBuyerCode", Buyer.intBuyerCode);
                com.Parameters.AddWithValue("@strBuyerName", Buyer.strBuyerName);
                com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                com.Parameters.AddWithValue("@intCreatedByCode", Convert.ToInt32(HttpContext.Current.Session["intUserCode"]));
                com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                com.Parameters.AddWithValue("@intModifyBy", Convert.ToInt32(HttpContext.Current.Session["intUserCode"]));
                com.Parameters.AddWithValue("@bolIsActive", Buyer.bolIsActive);


                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating Buyer record  
        public int Update(tblBuyer Buyer)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procInsertUpdateBuyer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@intBuyerCode", Buyer.intBuyerCode);
                com.Parameters.AddWithValue("@strBuyerName", Buyer.strBuyerName);
                com.Parameters.AddWithValue("@dtCreatedAt", DateTime.Now);
                com.Parameters.AddWithValue("@intCreatedByCode", Convert.ToInt32(HttpContext.Current.Session["intUserCode"]));
                com.Parameters.AddWithValue("@dtModifyAt", DateTime.Now);
                com.Parameters.AddWithValue("@intModifyBy", Convert.ToInt32(HttpContext.Current.Session["intUserCode"]));
                com.Parameters.AddWithValue("@bolIsActive", Buyer.bolIsActive);

                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an Buyer  
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procDeleteBuyer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}