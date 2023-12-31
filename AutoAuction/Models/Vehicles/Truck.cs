﻿using AutoAuction.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
    public class Truck : HeavyVehicle
    {
        public Truck(
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            bool hasTowbar,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType,
            VehicleDimensionsStruct vehicleDimensions,
            double LoadCapacity) : base(0, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, vehicleDimensions)
        {
            if (hasTowbar) { this.DriversLicense = DriversLicenseEnum.CE; }
            else { this.DriversLicense = DriversLicenseEnum.C; }
            this.VehicleDimensions = vehicleDimensions;
            this.LoadCapacity = LoadCapacity;
            //TODO: V10 - Constructor for Truck, DriversLisence should be CE if the truck has a towbar, otherwise it should be C
            //TODO: V11 - Add to database and set ID
        }
        public Truck(uint id) : base (id)
        {
            //TODO: REDO

            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetTruck {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.LoadCapacity = (double)reader.GetDouble(0);
                    }
                }
            }
        }

        /// <summary>
        /// Engine size 
        /// 
        /// 
        /// must be between 4.2 and 15.0 L or cast an out of range exection.
        /// </summary>
        /// <returns>The size the the engine as a double</returns>
        public override double EngineSize
        {
            get { return base.EngineSize; }
            set
            {
                //TODO: V10 - EngineSize must be between 4.2 and 15.0 L or cast an out of range exection.
                if (base.EngineSize < 4.2 && base.EngineSize > 15.0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                base.EngineSize = value;
            }
        }
        /// <summary>
        /// Load Capacity field and property
        /// </summary>
        public double LoadCapacity { get; set; }
        /// <summary>
        /// Returns the Truck in a string with relevant information.
        /// </summary>
        public override string ToString()
        {
            StringBuilder tsb = new StringBuilder($"{base.ToString()}, ");
            tsb.Append($"Load Capacity: {this.LoadCapacity}");
            //TODO: V12 - ToString for Truck
            return tsb.ToString();
        }

        public override void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddTruck '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0, " +
                $"{VehicleDimensions.Height.ToString(sqlCulture)}, {VehicleDimensions.Weight.ToString(sqlCulture)}, {VehicleDimensions.Length.ToString(sqlCulture)}, 0, " +
                $"{LoadCapacity.ToString(sqlCulture)}, 0");

            uint.TryParse(idFromDB, out uint id);
            ID = id;
        }
    }
}
