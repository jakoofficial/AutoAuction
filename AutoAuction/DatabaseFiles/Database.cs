using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace AutoAuction.DatabaseFiles
{
    public class Database
    {
        public static Database Instance { get; private set; }

        public SqlConnection con;
        public string ConnectionString = "Server=docker.data.techcollege.dk, 20001;" +
                                  "Database=AutoAuction;" +
                                  "User Id=Username;" +
                                  "Password=Password;";

        static Database()
        {
            Instance = new Database();
        }

        public static User GetUser(string username)
        {

            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetUser {username}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(1) == true)
                        {
                            return new CorporateUser(username, "", 
                                (uint)reader.GetInt32(2),
                                (uint)reader.GetInt32(6),
                                reader.GetDecimal(3),
                                reader.GetDecimal(4));
                        }
                        else if (reader.GetBoolean(1) == false)
                        {
                            return new PrivateUser(username, "", 
                            (uint)reader.GetInt32(2),
                            (uint)reader.GetInt32(6),
                            reader.GetDecimal(4));

                        }

                    }
                }
            }

            throw new ArgumentException(String.Format("{0} is not a recognized username!", username));
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
