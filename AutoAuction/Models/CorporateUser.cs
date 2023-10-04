using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class CorporateUser : User
    {
        public CorporateUser(string userName, string password, uint zipCode, uint cvrNummer, decimal credit, decimal balance ) : base(userName, password, zipCode, balance)
        {
            this.CVRNumber = cvrNummer;
            this.Credit = credit;
            
            //TODO: U7 - Set constructor 
            //TODO: U8 - Add to database and set ID
        }
        public uint CVRNumber { get; set; }
        public decimal Credit { get; set; }

        /// <summary>
        /// Checks if the CorporateUser is able to buy depending on their credit and balance
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public override bool AbleToBuy(decimal amount)
        {
            decimal minimumBalance = Balance + Credit;

            if ( amount > minimumBalance ) { return false; }

            return true;
        }
        public override string ToString()
        {
            StringBuilder tsb = new StringBuilder($"{base.ToString()}, ");
            tsb.Append($"CVRNumber: {this.CVRNumber}");
            tsb.Append($"Credit: {this.Credit}");
            return tsb.ToString();
        }
    }
}