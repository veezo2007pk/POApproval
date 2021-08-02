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
                        UserCode = rdr["UserCode"].ToString(),
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
        public int Add(userDataViewModel user, List<procGetAccessLevels_Result> lstMembersToNotify)
        {
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            String userCode = null;
            if (reqCookies != null)
            {
                userCode = reqCookies["intUserCode"].ToString();

            }
            else
            {
                userCode = HttpContext.Current.Session["intUserCode"].ToString();

            }

            int i;
            //if (user.bolIsApprovalLimit == null)
            //    user.bolIsApprovalLimit = false;

            //if (user.bolIsNewUser == null)
            //    user.bolIsNewUser = false;

            //if (user.bolIsManageBuyer == null)
            //    user.bolIsManageBuyer = false;

            //if (user.bolIsNewBuyer == null)
            //    user.bolIsNewBuyer = false;
            if (user.status == "1")
            {
                user.status = "ACTIVE";
            }
            else
            {
                user.status = "NOT ACTIVE";
            }
            if (user.SuperAdmin == "True")
            {
                user.SuperAdmin = "Y";
            }
            else
            {
                user.SuperAdmin = "N";
            }
            if (user.UserApprover == "True")
            {
                user.UserApprover = "Y";
            }
            else
            {
                user.UserApprover = "N";
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
                com.Parameters.AddWithValue("@pwd", user.pwd.Replace(" ", "%"));
                //com.Parameters.AddWithValue("@email", user.email);
                com.Parameters.AddWithValue("@xpertLoginID", user.xpertLoginID);
                //com.Parameters.AddWithValue("@usergroup", user.usergroup);
                //com.Parameters.AddWithValue("@bolIsApprovalLimit", user.bolIsApprovalLimit);
                //com.Parameters.AddWithValue("@bolIsNewUser", user.bolIsNewUser);
                //com.Parameters.AddWithValue("@bolIsManageBuyer", user.bolIsManageBuyer);
                //com.Parameters.AddWithValue("@bolIsNewBuyer", user.bolIsNewBuyer);
                com.Parameters.AddWithValue("@SuperAdmin", user.SuperAdmin);
                com.Parameters.AddWithValue("@UserApprover", user.UserApprover);
                com.Parameters.AddWithValue("@status", user.status);

                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();

                if (lstMembersToNotify.Count > 0)
                {
                    SqlCommand comm = new SqlCommand("procInsertUpdateUserAccess", con);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@usercode", user.usercode);
                    comm.Parameters.AddWithValue("@menu_code", 0);
                    comm.Parameters.AddWithValue("@created_by", userCode);

                    comm.Parameters.AddWithValue("@Action", "Delete");
                    i = comm.ExecuteNonQuery();
                    foreach (var item in lstMembersToNotify)
                    {
                        if (item.menuCode != 0)
                        {
                            SqlCommand comm1 = new SqlCommand("procInsertUpdateUserAccess", con);
                            comm1.CommandType = CommandType.StoredProcedure;
                            comm1.Parameters.AddWithValue("@usercode", user.usercode);
                            comm1.Parameters.AddWithValue("@menu_code", item.menuCode);
                            comm1.Parameters.AddWithValue("@created_by", userCode);
                            comm1.Parameters.AddWithValue("@Action", "Insert");
                            i = comm1.ExecuteNonQuery();
                        }

                    }
                }
            }
            return i;
        }

        //Method for Updating User record  
        public int Update(userDataViewModel user, List<procGetAccessLevels_Result> lstMembersToNotify)
        {
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            String userCode = null;
            if (reqCookies != null)
            {
                userCode = reqCookies["intUserCode"].ToString();

            }
            else
            {
                userCode = HttpContext.Current.Session["intUserCode"].ToString();

            }
            int i;
            //if (user.bolIsApprovalLimit == null)
            //    user.bolIsApprovalLimit = false;

            //if (user.bolIsNewUser == null)
            //    user.bolIsNewUser = false;

            //if (user.bolIsManageBuyer == null)
            //    user.bolIsManageBuyer = false;

            //if (user.bolIsNewBuyer == null)
            //    user.bolIsNewBuyer = false;
            if (user.status == "1")
            {
                user.status = "ACTIVE";
            }
            else
            {
                user.status = "NOT ACTIVE";
            }
            if (user.SuperAdmin == "True")
            {
                user.SuperAdmin = "Y";
            }
            else
            {
                user.SuperAdmin = "N";
            }
            if (user.UserApprover == "True")
            {
                user.UserApprover = "Y";
            }
            else
            {
                user.UserApprover = "N";
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
                com.Parameters.AddWithValue("@pwd", user.pwd.Replace(" ", "%"));
                //com.Parameters.AddWithValue("@email", user.email);
                com.Parameters.AddWithValue("@xpertLoginID", user.xpertLoginID);
                //com.Parameters.AddWithValue("@usergroup", user.usergroup);
                //com.Parameters.AddWithValue("@bolIsApprovalLimit", user.bolIsApprovalLimit);
                //com.Parameters.AddWithValue("@bolIsNewUser", user.bolIsNewUser);
                //com.Parameters.AddWithValue("@bolIsManageBuyer", user.bolIsManageBuyer);
                //com.Parameters.AddWithValue("@bolIsNewBuyer", user.bolIsNewBuyer);
                com.Parameters.AddWithValue("@SuperAdmin", user.SuperAdmin);
                com.Parameters.AddWithValue("@UserApprover", user.UserApprover);
                com.Parameters.AddWithValue("@status", user.status);


                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
                if (lstMembersToNotify.Count > 0)
                {
                    SqlCommand comm = new SqlCommand("procInsertUpdateUserAccess", con);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@usercode", user.usercode);
                    comm.Parameters.AddWithValue("@menu_code", 0);
                    comm.Parameters.AddWithValue("@created_by", userCode);
                    comm.Parameters.AddWithValue("@Action", "Delete");
                    i = comm.ExecuteNonQuery();
                    foreach (var item in lstMembersToNotify)
                    {
                        if (item.menuCode != 0)
                        {
                            SqlCommand comm1 = new SqlCommand("procInsertUpdateUserAccess", con);
                            comm1.CommandType = CommandType.StoredProcedure;
                            comm1.Parameters.AddWithValue("@usercode", user.usercode);
                            comm1.Parameters.AddWithValue("@menu_code", item.menuCode);
                            comm1.Parameters.AddWithValue("@created_by", userCode);
                            comm1.Parameters.AddWithValue("@Action", "Insert");
                            i = comm1.ExecuteNonQuery();
                        }

                    }
                }
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