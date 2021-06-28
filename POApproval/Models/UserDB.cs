using POApproval.GlobalInfo;
using POApproval.Models;
using POApproval.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POApproval.Models
{
    public class UserDB
    {

        //Return list of all Users  
        public List<procSelectUser_Result> ListAll()
        {
            List<procSelectUser_Result> lst = new List<procSelectUser_Result>();
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procSelectUser", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new procSelectUser_Result
                    {
                        UserCode= rdr["UserCode"].ToString(),
                        Username = rdr["Username"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Department = rdr["Department"].ToString(),
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

                intCode = DAL.getNext_code("Select ISNULL(MAX(intUserCode), 0) + 1 from tblUser");



            }

            catch (Exception ex)

            {



                throw;

            }

            return intCode;



        }

        //Method for Adding an User  
        public int Add(userDataViewModel user)
        {

            int i;
            if (user.bolIsApprovalLimit == null)
                user.bolIsApprovalLimit = false;

            if (user.bolIsNewUser == null)
                user.bolIsNewUser = false;

            if (user.bolIsManageBuyer == null)
                user.bolIsManageBuyer = false;

            if (user.bolIsNewBuyer == null)
                user.bolIsNewBuyer = false;
            if (user.status == "1")
            {
                user.status = "ACTIVE";
            }
            else
            {
                user.status = "NOT ACTIVE";
            }
            if (user.SuperAdmin == "1")
            {
                user.SuperAdmin = "Y";
            }
            else
            {
                user.SuperAdmin = "N";
            }

            //Int32 intUserCodeMaxCode = (Int32)get_nextCode();
            //if (intUserCodeMaxCode == 0)
            //    intUserCodeMaxCode = 1;
            

            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procInsertUpdateUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@usercode", user.usercode);
                //com.Parameters.AddWithValue("@fullname", user.fullname);
                com.Parameters.AddWithValue("@pwd", user.pwd);
                //com.Parameters.AddWithValue("@email", user.email);
                com.Parameters.AddWithValue("@xpertLoginID", user.xpertLoginID);
                //com.Parameters.AddWithValue("@usergroup", user.usergroup);
                com.Parameters.AddWithValue("@bolIsApprovalLimit", user.bolIsApprovalLimit);
                com.Parameters.AddWithValue("@bolIsNewUser", user.bolIsNewUser);
                com.Parameters.AddWithValue("@bolIsManageBuyer", user.bolIsManageBuyer);
                com.Parameters.AddWithValue("@bolIsNewBuyer", user.bolIsNewBuyer);
                com.Parameters.AddWithValue("@SuperAdmin", user.SuperAdmin);
                com.Parameters.AddWithValue("@status", user.status);
            
          
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating User record  
        public int Update(userDataViewModel user)
        {
            int i;
            if (user.bolIsApprovalLimit == null)
                user.bolIsApprovalLimit = false;

            if (user.bolIsNewUser == null)
                user.bolIsNewUser = false;

            if (user.bolIsManageBuyer == null)
                user.bolIsManageBuyer = false;

            if (user.bolIsNewBuyer == null)
                user.bolIsNewBuyer = false;
            if (user.status == "1")
            {
                user.status = "ACTIVE";
            }
            else
            {
                user.status = "NOT ACTIVE";
            }
            if (user.SuperAdmin == "1")
            {
                user.SuperAdmin = "Y";
            }
            else
            {
                user.SuperAdmin = "N";
            }

            //Int32 intUserCodeMaxCode = (Int32)get_nextCode();
            //if (intUserCodeMaxCode == 0)
            //    intUserCodeMaxCode = 1;


            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procInsertUpdateUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@usercode", user.usercode);
                //com.Parameters.AddWithValue("@fullname", user.fullname);
                com.Parameters.AddWithValue("@pwd", user.pwd);
                //com.Parameters.AddWithValue("@email", user.email);
                com.Parameters.AddWithValue("@xpertLoginID", user.xpertLoginID);
                //com.Parameters.AddWithValue("@usergroup", user.usergroup);
                com.Parameters.AddWithValue("@bolIsApprovalLimit", user.bolIsApprovalLimit);
                com.Parameters.AddWithValue("@bolIsNewUser", user.bolIsNewUser);
                com.Parameters.AddWithValue("@bolIsManageBuyer", user.bolIsManageBuyer);
                com.Parameters.AddWithValue("@bolIsNewBuyer", user.bolIsNewBuyer);
                com.Parameters.AddWithValue("@SuperAdmin", user.SuperAdmin);
                com.Parameters.AddWithValue("@status", user.status);


                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an User  
        public string Delete(string ID)
        {
            string i;
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procDeleteUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@intUserCode", ID);
                i = com.ExecuteNonQuery().ToString();
            }
            return i;
        }
    }
}