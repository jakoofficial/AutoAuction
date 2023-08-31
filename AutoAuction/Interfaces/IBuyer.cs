using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Interfaces
{
    public interface IBuyer
    {
        /// <summary>
        /// UserName proberty
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Balance proberty
        /// </summary>
        decimal Balance { get; set; }
    }
}
