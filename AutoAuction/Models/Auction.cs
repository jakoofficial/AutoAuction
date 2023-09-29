using AutoAuction.DatabaseFiles;
using AutoAuction.Interfaces;
using AutoAuction.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public Auction(uint id)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);

            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetAuction {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.MinimumPrice = reader.GetDecimal(1);
                        this.StandingBid = reader.GetDecimal(2);
                        SqlCommand vHeavyType = new SqlCommand($"exec GetHeavyVehicle {reader.GetInt32(3)}", con);
                        SqlCommand vPersonalType = new SqlCommand($"exec GetPersonalCar {reader.GetInt32(3)}", con);

                        if (vHeavyType != null)
                        {
                            Truck t = new Truck((uint)reader.GetInt32(3));
                            Bus b = new Bus((uint)reader.GetInt32(3));

                            if (t != null) { this.Vehicle = t; }
                            if (b != null) { this.Vehicle = b; }

                            Debug.WriteLine(this.Vehicle);

                            //using (SqlDataReader vReader = vHeavyType.ExecuteReader())
                            //{
                            //    while (vReader.Read())
                            //    {
                            //        //TODO: REquires functionality

                            //    }
                            //}
                        }

                        //this.Vehicle = new Truck(reader.GetDecimal(3));
                    }
                }
            }
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

        //public override string ToString()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
