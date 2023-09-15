using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class PrivateUser : User
    {
        public PrivateUser(string userName, string password, uint zipCode, uint cprNummer) : base(userName, password, zipCode)
        {
            //TODO: U10 - Set constructor
            //TODO: U11 - Add to database and set ID
            //throw new NotImplementedException();
        }
        public uint CPRNumber { get; set; }

    }
}
