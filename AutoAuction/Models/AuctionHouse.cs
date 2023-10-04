using AutoAuction.DatabaseFiles;
using AutoAuction.Interfaces;
using AutoAuction.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    public delegate string NodificationDelegate(string message);
    public static class AuctionHouse
    {
        public static ObservableCollection<Auction> Auctions = new ObservableCollection<Auction>();
        public static ObservableCollection<Vehicle> SoldVehicles = new ObservableCollection<Vehicle>();

        public static ObservableCollection<Auction> GetAllAuctions()
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"EXEC GetAllAuctions", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Auction a = new Auction(Auction.GetAuctionVehicle((uint)reader.GetInt32(3)), Database.Instance.GetUser(reader.GetString(4)),
                            Database.Instance.GetUser(reader.GetString(5)),reader.GetDecimal(1), reader.GetDecimal(2), reader.GetBoolean(6));
                        Auctions.Add(a);
                        a = null;
                    }
                }
            }

            return Auctions;
        }

        /// <summary>
        /// A method that ...
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="seller"></param>
        /// <param name="miniumBid"></param>
        /// <returns> Auction ID </returns>
        public static uint SetForSale(Vehicle vehicle, ISeller seller, decimal miniumBid)
        {
            //TODO: A3 - SetForSale
            string temp = seller.UserName;
            //TODO: Create auction, upload to db, give auctionID back.
            //Use Recieve bid to check and send notification to seller if bid is over min price.
            string auctionNumber = Database.Instance.ExecScalar($"EXEC CreateAuction {miniumBid}, 0, {vehicle.ID},'{seller.UserName}', '', {true}");
            return Convert.ToUInt32(auctionNumber);
        }

        //UpdateAuction
        public static void UpdateAuction(string buyerName, uint auctionID, decimal bid, bool activeStage)
        {
            //$"exec UpdateAuction {id}", con;
            Database.Instance.ExecNonQuery($"exec UpdateAuction {auctionID}, '{buyerName}', {bid.ToString(new CultureInfo("en-US"))}, {activeStage}");

        }

        /// <summary>
        /// Recieves a bid from a buyer.
        /// Checks if the bid is eligable, by ...
        /// </summary>
        /// <param name="buyer">identification for the potential buyer placeing a bid.</param>
        /// <param name="auctionID">Used to find the auction.</param>
        /// <param name="bid">The bid in decimal with the value ending in M for money.</param>
        /// <returns> A bool that indicats whether a bid was recieved or rejected. </returns>
        public static bool RecieveBid(IBuyer buyer, uint auctionID, decimal bid)
        {
            //TODO: A5 - RecieveBid
            //Get Auction, Check if buyer has enough money, check if bid is higher than current bid / Min price.
            //If bid is accepted, send notification to seller.
            Auction TempA = null;
            foreach (Auction a in Auctions)
            {
                if (a.ID == auctionID)
                { TempA = a; break; }
            }
            if (TempA == null) return false;
            
            if (buyer.Balance > TempA.StandingBid && bid > TempA.StandingBid)
            {
                UpdateAuction(buyer.UserName, auctionID, bid, false);
                return true;
            }

            return false;
        }
        /// <summary>
        /// Accepts a bid and ...
        /// </summary>
        /// <param name="seller"></param>
        /// <param name="auctionID"></param>
        /// <returns></returns>
        public static bool AcceptBid(ISeller seller, uint auctionID)
        {
            //TODO: A6 - AcceptBid
            // Check seller if is seller, Removed money from buyer,
            // Removed from list of Auctions and add to list of sold Vehicles
            // List of sold vehicles should be accesable from everywhere.
            // Return if sell went through
            Auction a = null;
            //Finds the auction
            foreach (var auction in Auctions)
            {
                if (auction.ID == auctionID)
                { a = auction; break; }
            }

            if (seller.UserName != a.Seller.UserName)
            { throw new Exception($"{seller.UserName} is not the seller of this product"); return false; }

            if (a.Buyer != null && a.Buyer.Balance >= a.StandingBid && a.Active) 
            { 
                a.Buyer.Balance -= a.StandingBid;
                Auctions.Remove(a);
                SoldVehicles.Add(a.Vehicle);

                UpdateAuction(a.Buyer.UserName, a.ID, a.StandingBid, false);
                User.Instance.UpdateBalance(a.Buyer.UserName, a.Buyer.Balance);
                return true;
            }

            return false;
        }
        #region Search Methods
        /// <summary>
        /// Find an auction in the auction list from the id using a binary search.
        /// </summary>
        /// <param name="auctionID"></param>
        /// <returns> The Auction with the specific id or null if not found </returns>
        public static Auction FindAuctionByID(uint auctionID)
        {
            //TODO: A7 - FindAuctionByID
            foreach (var auction in AuctionHouse.Auctions)
            {
                if (auctionID == auction.ID)
                {
                    return auction;
                }
            }

            throw new ArgumentException($"Auction with the id of {auctionID}, was not found.");
        }
        /// <summary>
        /// Finds vehicles by the name or part of the name.
        /// Ekstra Opgave: 1. Find køretøjer hvis navn indeholder en angivet søgestreng
        /// </summary>
        /// <param name="searchWord"></param>
        /// <returns> A list of vehicles that contains the search word </returns>
        public static async Task<List<Vehicle>> FindVehiclesByName(string searchWord)
        {
            //TODO: AS1 - FindVehiclesByName
            throw new NotImplementedException();
        }
        /// <summary>
        /// Finds vehicles based on the minimum number of seats and whether it has toilet facilities.
        /// Ekstra Opgave: 2. Find køretøjer der har et minimum angivet antal siddepladser samt toiletfaciliteter.
        /// </summary>
        /// <param name="seats"></param>
        /// <param name="hasToilet"></param>
        /// <returns> A list of vehicles that contains the vehicles </returns>
        public static async Task<List<Vehicle>> FindVehiclesByNumberofSeats(int seats, bool hasToilet)
        {
            //TODO: AS2 - FindVehiclesByNumberofSeats
            throw new NotImplementedException();
        }
        /// <summary>
        /// Ekstra Opgave: 3. Find køretøjer der kræver stort kørekort (kategori C, D, CE eller DE) og vejer under en angivet maksimalvægt
        /// </summary>
        /// <param name="maxWeight"></param>
        /// <returns> A list of vehicles that contains the vehicles </returns>
        public static async Task<List<Vehicle>> FindVehiclesByDriversLisence(double maxWeight)
        {
            //TODO: AS3 - FindVehiclesByDriversLisence
            throw new NotImplementedException();
        }
        /// <summary>
        /// Ekstra Opgave: 4. Find alle personbiler til privatbrug som har kørt under et angivet antal km, og hvor minimum salgsprisen
        /// samtidig ligger under et angivet beløb. Køretøjerne skal returneres i sorteret rækkefølge efter antal kørte km.
        /// </summary>
        /// <param name="maxKm"></param>
        /// <param name="maxPrice"></param>
        /// <returns> A list of vehicles that contains the vehicles under maxKm and maxPrince </returns>
        public static async Task<List<Vehicle>> FindVehiclesByKmAndPrice(double maxKm, decimal maxPrice)
        {
            //TODO: AS4 - FindVehiclesByKmAndPrice
            throw new NotImplementedException();
        }
        /// <summary>
        /// Ekstra Opgave: 5. Find alle køretøjer hvor køretøjets sælger er bosiddende inden for en bestemt radius af et angivet 
        /// postnummer. I denne forbindelse kan radius blot anskues som et tal der skal lægges til/trækkes fra
        /// postnummeret.F.eks.vil en søgning efter køretøjer inden for en radius af 1500 fra postnummer 8000,
        /// inkludere alle køretøjer hvor sælgers postnummer ligger mellem 6500 og 9500.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <param name="range"></param>
        /// <returns> A list of sellers that contains the sellers in range of the zipcode </returns>
        public static async Task<List<ISeller>> FindSellersByZipcodeRange(uint zipCode, uint range)
        {
            //TODO: AS5 - FindSellersByZipcodeRange
            throw new NotImplementedException();
        }
        #endregion
    }
}
