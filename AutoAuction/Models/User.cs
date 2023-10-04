using AutoAuction.DatabaseFiles;
using AutoAuction.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AutoAuction.Models.Vehicles.Vehicle;

namespace AutoAuction.Models
{
    /*
     * Domæne model
    interface polymorfi via interface
    interface til at kunne køde og sælge til

    køber og sælger som interfaces

    privat og company som klasser
     */

    public abstract class User : ISeller, IBuyer //TODO: U4 - Implement interfaces
    {
        public static User Instance { get; private set; }

        public static void SetUser(User u)
        {
            Instance = u;
        }

        protected User(string userName, string password, uint zipCode)
        {
            this.UserName = userName;
            this.Password = password;
            this.ZipCode = zipCode;

            //TODO: U1 - Set constructor and field
            makePasswordHash(password);
        }

        /// <summary>
        /// Construntor for a Buyer and Seller
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="zipCode"></param>
        /// <param name="balance"></param>
        protected User(string userName, string password, uint zipCode, decimal balance)
        {
            this.UserName = userName;
            this.Password = password;
            this.ZipCode = zipCode;
            this.Balance = balance;
            makePasswordHash(password);
        }

        protected User(string userName)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetUser {userName}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.UserName = reader.GetString(1);
                        this.Zipcode = (uint)reader.GetInt32(3);
                        this.Balance = reader.GetDecimal(5);
                    }
                }
            }
        }

        public void UpdateBalance(string username, decimal newBalance)
        {
            try
            {
                Database.Instance.ExecNonQuery($"exec UpdateBalance {username}, {newBalance.ToString(new CultureInfo("en-US"))}");
            }
            catch
            {
                throw new Exception("That is not possible");
            }
        }

        /// <summary>
        /// Construntor for a User - Buyer 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="zipCode"></param>
        /// <param name="balance"></param>
        public virtual bool AbleToBuy(decimal amount)
        {
            decimal minimumBalance = Balance;

            if (amount > minimumBalance) { return false; }

            return true;
        }

        private bool makePasswordHash(string password)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            PasswordHash = result;

            return (PasswordHash == result);
        }
        /// <summary>
        /// ID property
        /// </summary>
        public uint ID { get; private set; }
        public string UserName { get; set; }
        public string Password { get; private set; }
        public uint ZipCode { get; private set; }
        /// <summary>
        /// PasswordHash property
        /// </summary>
        private byte[] PasswordHash { get; set; }
        public virtual decimal Balance { get; set; }

        public uint Zipcode { get; set; }

        /// <summary>
        /// A method that ...
        /// </summary>
        /// <returns>Whether login is valid</returns>
        public bool ValidateLogin(string loginUserName, string loginPassword)
        {
            //TODO: U5 - Implement the rest of validation for password and user name
            //min 10 characters, max 25 chars, uppercase, special character
            HashAlgorithm sha = SHA256.Create(); //Make a HashAlgorithm object for makeing hash computations.
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(loginPassword)); //Encodes the password into a hash in a Byte array.

            return PasswordHash == result;

            //throw new NotImplementedException();
        }

        //TODO: U4 - Implement interface proberties and methods.


        /// <summary>
        /// Returns the User in a string with relivant information.
        /// </summary>
        /// <returns>...</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ID: {ID}, ");
            sb.Append($"Username: {UserName}, ");
            sb.Append($"Password: {Password}, ");
            sb.Append($"ZipCode: {ZipCode}, ");
            sb.Append($"PasswordHash: {PasswordHash}, ");
            sb.Append($"balance: {Balance}");
            return sb.ToString();
            //TODO: U3 - ToString for 
        }

        public string ReceiveBidNotification(string message)
        {
            throw new NotImplementedException();
        }
    }
}
