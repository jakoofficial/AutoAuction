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
                        this.ID = (uint)reader.GetInt32(0);
                        this.MinimumPrice = reader.GetDecimal(1);
                        this.StandingBid = reader.GetDecimal(2);
                        this.Seller = Database.Instance.GetUser(reader.GetString(4));

                        if (Database.Instance.GetUser(reader.GetString(5)) != null)
                        {
                            this.Buyer = Database.Instance.GetUser(reader.GetString(5));
                        }
                        else
                        {
                            this.Buyer = null;
                        }

                        SqlCommand vHeavyType = new SqlCommand($"exec GetHeavyVehicle {reader.GetInt32(3)}", con);
                        SqlCommand vPersonalType = new SqlCommand($"exec GetPersonalCar {reader.GetInt32(3)}", con);

                        if (vHeavyType != null)
                        {
                            Truck t = new Truck((uint)reader.GetInt32(3));
                            Bus b = new Bus((uint)reader.GetInt32(3));

                            if (t.LoadCapacity != 0)
                            {
                                this.Vehicle = t;
                                t = null; b = null;
                            }
                            else
                            {
                                this.Vehicle = b;
                                b = null; t = null;
                            }

                            PrivatePersonalCar pripc = new PrivatePersonalCar((uint)reader.GetInt32(3));
                            ProfessionalPersonalCar propc = new ProfessionalPersonalCar((uint)reader.GetInt32(3));

                            //if (pripc.Load) { }
                            if (propc.LoadCapacity != 0)
                            {
                                this.Vehicle = propc;
                                propc = null; pripc = null;
                            }
                            else
                            {
                                this.Vehicle = pripc;
                                pripc = null; propc = null;
                            }
                        }

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
        internal IBuyer? Buyer { get; set; }

        //public override string ToString()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
