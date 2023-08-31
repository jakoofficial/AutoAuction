using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class CorporateUser : User
    {
        public CorporateUser(string userName, string password, uint zipCode, uint cvrNummer, decimal credit) : base(userName, password, zipCode)
        {
            //TODO: U7 - Set constructor
            //TODO: U8 - Add to database and set ID
            throw new NotImplementedException();
        }
        public uint CVRNumber { get; set; }
        public decimal Credit { get; set; }
    }
}
