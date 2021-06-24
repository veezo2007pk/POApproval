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
        public int Add(tblUser user)
        {

            int i;
            if (user.bolIsApprovalLimit == null)
                user.bolIsApprovalLimit = false;
            if (user.bolIsNewUser == null)
                user.bolIsNewUser = false;
            if (user.bolIsActive == null)
                user.bolIsActive = false;

            Int32 intUserCodeMaxCode = (Int32)get_nextCode();
            if (intUserCodeMaxCode == 0)
                intUserCodeMaxCode = 1;
            

            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procInsertUpdateUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@intUserCode", user.intUserCode);
                com.Parameters.AddWithValue("@logon_user_id", user.logon_user_id);
                com.Parameters.AddWithValue("@logon_user_name", user.logon_user_id);
                com.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                com.Parameters.AddWithValue("@email", user.email);
                com.Parameters.AddWithValue("@strDepartmentName", user.strDepartmentName);
                com.Parameters.AddWithValue("@bolIsApprovalLimit", user.bolIsApprovalLimit);
                com.Parameters.AddWithValue("@bolIsNewUser", user.bolIsNewUser);
                com.Parameters.AddWithValue("@bolIsAdmin", user.SuperAdmin);
                com.Parameters.AddWithValue("@bolIsActive", user.bolIsActive);
            
          
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating User record  
        public int Update(tblUser user)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procInsertUpdateUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@intUserCode", user.intUserCode);
                com.Parameters.AddWithValue("@logon_user_id", user.logon_user_id);
                com.Parameters.AddWithValue("@logon_user_name", user.logon_user_id);
                com.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                com.Parameters.AddWithValue("@email", user.email);
                com.Parameters.AddWithValue("@strDepartmentName", user.strDepartmentName);
                com.Parameters.AddWithValue("@bolIsApprovalLimit", user.bolIsApprovalLimit);
                com.Parameters.AddWithValue("@bolIsNewUser", user.bolIsNewUser);
                com.Parameters.AddWithValue("@bolIsActive", user.bolIsActive);

               com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an User  
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("procDeleteUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}