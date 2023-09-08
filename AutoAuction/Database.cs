using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction
{
    public class Database
    {
        public static Database Instance { get; private set; }

        public SqlConnection con;
        public string ConnectionString = "Server=docker.data.techcollege.dk, 20001;" +
                                  "Database=AutoAuction;" +
                                  "User Id=sa;" +
                                  "Password=H2PD071123_Gruppe1;";

        static Database()
        {
            Instance = new Database();
        }

        public string ExecScalar(string command)
        {
            string val = "";
            con = new SqlConnection(ConnectionString);
            con.Open();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                con.Open();
                val = (string)cmd.ExecuteScalar().ToString();
            }
            con.Close();
            return val;
        }

        public void ExecNonQuery(string command)
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}
