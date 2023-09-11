using AutoAuction.Interfaces;
using AutoAuction.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public class Auction
    {
        /// <summary>
        /// Constructor for Auction
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="seller"></param>
        /// <param name="minimumPrice"></param>
        public Auction(Vehicle vehicle, ISeller? seller, decimal minimumPrice)
        {
            //TODO: A1 - Set constructor
            //ID set from DB
            this.Vehicle = vehicle;
            this.Seller = seller;
            this.MinimumPrice = minimumPrice;
            //TODO: A2 - Add to database and set ID
        }
        /// <summary>
        /// ID of the auction
        /// </summary>
        public uint ID { get; private set; }
        /// <summary>
        /// The minimum price of the auction
        /// </summary>
        public decimal MinimumPrice { get; set; }
        /// <summary>
        /// The standing bid of the auction
        /// </summary>
        public decimal StandingBid { get; set; }
        /// <summary>
        /// The vehicle of the auction
        /// </summary>
        internal Vehicle Vehicle { get; set; }
        /// <summary>
        /// The seller of the auction
        /// </summary>
        internal ISeller Seller { get; set; }
        /// <summary>
        /// The buyer or potential buyer of the auction
        /// </summary>
        internal IBuyer Buyer { get; set; }

        public int SetForSale(Vehicle vechicle, ISeller seller, decimal minPris)
        {
            return 0;
        }
        
        public int SetForSale(Vehicle vechicle, ISeller seller, decimal minPris, string notificationMessage)
        {
            //TODO: Add the notification function
            return 0;
        }

        public bool RecieveBid(IBuyer buyer, int actionNumber, decimal bid)
        {
            return false;
        }

        public bool AcceptBid(ISeller seller, int auctionNumber)
        {
            return false;
        }

        public static Auction FindAuctionById(uint auctionID)
        {
            foreach (var auction in AuctionHouse.Auctions)
            {
                if (auctionID == 1)
                {
                    return auction;
                }
            }

            return null;
        }

        //public override string ToString()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
