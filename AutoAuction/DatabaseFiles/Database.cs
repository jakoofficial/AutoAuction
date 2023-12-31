﻿using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
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
        internal string ConnectionString = "";

        static Database()
        {
            Instance = new Database();
        }

        /// <summary>
        /// GetUser method will get the different users depending on the username. 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Corporate or Private user</returns>
        /// <exception cref="ArgumentException">If username is not recognized</exception>
        public User GetUser(string username)
        {

            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                try
                {
                    SqlCommand command = new SqlCommand($"exec GetUserOfType {username}", con);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetBoolean(1) == true)
                            {
                                return new CorporateUser(username, "",
                                    (uint)reader.GetInt32(2),
                                    reader.GetString(5),
                                    reader.GetDecimal(3),
                                    reader.GetDecimal(4));
                            }
                            else if (reader.GetBoolean(1) == false)
                            {
                                return new PrivateUser(username, "",
                                (uint)reader.GetInt32(2),
                                reader.GetString(4),
                                reader.GetDecimal(3));
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw new ArgumentException(String.Format("{0} is not a recognized username!", username));
                    return null;
                }
            }
            return null;
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

        /// <summary>
        /// tries to connect to the database with the given credentials, if the login succeeds the ConnectionString will be updated accordingly
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns> Return true if the login succeeds </returns>
        public bool LogInWithUser(string username, string password)
        {

            ConnectionString = "Server=docker.data.techcollege.dk, 20001;" +
                               "Database=AutoAuction;" +
                               $"User Id={username};" + //User Id = sa
                               $"Password={password};"; //Password=H2PD071123_Gruppe1

            try
            {
                ExecNonQuery($"EXEC GetUser {username}");
                return true;
            }
            catch (Exception)
            {
                ConnectionString = "";
                return false;
                throw;
            }
        }

        /// <summary>
        /// Add a PrivateUser to the database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Returns a string with the error of there is one </returns>
        public string? CreateUser(PrivateUser user)
        {
            LogInWithUser("sa", "H2PD071123_Gruppe1");

            try
            {
                ExecNonQuery($"EXEC CreatePrivateUser '{user.UserName}', '{user.Password}', {user.ZipCode}, '{user.CPRNumber}'");
                ConnectionString = "";
                return null;
            }
            catch (SqlException e)
            {
                ConnectionString = "";
                return e.Message;
                throw;
            }
        }

        /// <summary>
        /// Add a CorporateUser to the database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Returns a string with the error of there is one </returns>
        public string? CreateUser(CorporateUser user)
        {
            LogInWithUser("sa", "H2PD071123_Gruppe1");
            try
            {
                ExecNonQuery($"CreateCorporateUser '{user.UserName}', '{user.Password}', {user.ZipCode}, '{user.CVRNumber}', {user.Credit.ToString(new CultureInfo("en-US"))}");
                ConnectionString = "";
                return null;
            }
            catch (SqlException e)
            {
                ConnectionString = "";
                return e.Message;
                throw;
            }
        }
    }
}
