﻿using AutoAuction.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
    public class PrivatePersonalCar : PersonalCar
    {
        public PrivatePersonalCar(
            uint id,
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            bool hasTowbar,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType,
            ushort numberOfSeat,
            TrunkDimensionsStruct trunkDimensions,
            bool hasIsofixFittings)
            : base(id, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeat, trunkDimensions)
        {
            //TODO: V19 - PrivatePersonalCar constructor. DriversLicense should be 'B'
            this.HasIsofixFittings = hasIsofixFittings;
            //TODO: V20 - Add to database and set ID GetPrivatePersonalCar
            //throw new NotImplementedException();
        }

        public PrivatePersonalCar(uint id) : base(id)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetPrivatePersonalCar {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.HasIsofixFittings = reader.GetBoolean(1);
                    }
                }
            }
        }
        /// <summary>
        /// Isofix Fittings 
        /// 
        /// </summary>
        public bool HasIsofixFittings { get; set; }
        /// <summary>
        /// Returns the PrivatePersonalCar in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            //TODO: V21 - ToString for PrivatePersonalCar
            StringBuilder psb = new StringBuilder($"{base.ToString()}, ");
            string isoRes = "No";
            if (this.HasIsofixFittings){ isoRes = "Yes"; }

            psb.Append($"Isofix: {isoRes}");
            //TODO: V12 - ToString for Truck
            return psb.ToString();
        }

        public override void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddPrivatePersonalCar '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0, " +
                $"{NumberOfSeat}, {TrunkDimensions.Height.ToString(sqlCulture)}, {TrunkDimensions.Width.ToString(sqlCulture)}, {TrunkDimensions.Depth.ToString(sqlCulture)}, 0, " +
                $"{HasIsofixFittings}, 0");

            uint.TryParse(idFromDB, out uint id);
            ID = id;
        }
    }
}
