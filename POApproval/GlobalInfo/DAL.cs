using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace POApproval.GlobalInfo
{
    public static class DAL
    {
        public static int getNext_code(object customdata)

        {
            using (SqlConnection con = new SqlConnection(ConnectionString.cs))

            {

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;

                con.Open();

                cmd.CommandText = customdata.ToString();

                int Code = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                return Code;



            }

        }
    }
}