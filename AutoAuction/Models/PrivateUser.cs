using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class PrivateUser : User
    {
        public PrivateUser(string userName, string password, uint zipCode, uint cprNummer, decimal balance) : base(userName, password, zipCode, balance)
        {
            this.CPRNumber = cprNummer;
            this.Balance = balance;
            //TODO: U10 - Set constructor
            //TODO: U11 - Add to database and set ID
        }
        public uint CPRNumber { get; set; }
        private decimal _balance;

        public new decimal Balance
        {
            get { return _balance; }
            set
            {
                if (value < 0)
                {
                    //TODO: if the number is too small
                }
                else
                {
                    _balance = value;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder tsb = new StringBuilder($"{base.ToString()}, ");
            tsb.Append($"CVRNumber: {this.CPRNumber}");
            //TODO: CHECK if balance shows corretly from User.
            return tsb.ToString();
        }
    }
}
