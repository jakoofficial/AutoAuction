using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoAuction.Models
{
    /*
     * Domæne model
    interface polymorfi via interface
    interface til at kunne køde og sælge til

    køber og sælger som interfaces

    privat og company som klasser
     */

    public abstract class User //TODO: U4 - Implement interfaces
    {
        protected User(string userName, string password, uint zipCode)
        {
            this.UserName = userName;
            this.Password = password;
            this.ZipCode = zipCode;

            //TODO: U1 - Set constructor and field

            HashAlgorithm sha = SHA256.Create();
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            PasswordHash = result;

            throw new NotImplementedException();
        }
        /// <summary>
        /// ID property
        /// </summary>
        public uint ID { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public uint ZipCode { get; private set; }
        /// <summary>
        /// PasswordHash property
        /// </summary>
        private byte[] PasswordHash { get; set; }
        /// <summary>
        /// A method that ...
        /// </summary>
        /// <returns>Whether login is valid</returns>
        private bool ValidateLogin(string loginUserName, string loginPassword)
        {
            //TODO: U5 - Implement the rest of validation for password and user name

            HashAlgorithm sha = SHA256.Create(); //Make a HashAlgorithm object for makeing hash computations.
            byte[] result = sha.ComputeHash(Encoding.ASCII.GetBytes(loginPassword)); //Encodes the password into a hash in a Byte array.

            return PasswordHash == result;

            throw new NotImplementedException();
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
            return sb.ToString();
            //TODO: U3 - ToString for 
        }
    }
}
