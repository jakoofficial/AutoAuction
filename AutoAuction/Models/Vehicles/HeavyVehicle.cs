﻿using AutoAuction.DatabaseFiles;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
    public abstract class HeavyVehicle : Vehicle
    {
        public HeavyVehicle(
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
         VehicleDimensionsStruct vehicleDimension) 
         : base(id, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType)
        {
            this.VehicleDimensions = vehicleDimension;
        }
        public HeavyVehicle(uint id) :base(id)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetHeavyVehicle {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.VehicleDimensions = new VehicleDimensionsStruct(reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(3));
                    }
                }
            }
        }
        /// <summary>
        /// Vehicle dimensions
        /// and struct
        /// </summary>
        public VehicleDimensionsStruct VehicleDimensions { get; set; }
        /// <summary>
        /// The dimensions of the vehicle in meters.
        /// </summary>
        public struct VehicleDimensionsStruct
        {
            public VehicleDimensionsStruct(double height, double weight, double length)
            {
                Height = height;
                Weight = weight;
                Length = length;
            }
            public double Height { get; }
            public double Weight { get; }
            public double Length { get; }
            public override string ToString() => $"(Height: {Height}, Weight: {Weight}, Depth: {Length})";
        }
        /// <summary>
        /// Returns the HeavyVehicle in a string with relevant information.
        /// </summary>
        ///
        public override string ToString()
        {
            StringBuilder VehicleString = new StringBuilder($"{base.ToString()}, ");
            VehicleString.Append($"Vehicle Dimensions: {VehicleDimensions.ToString()}");
            return VehicleString.ToString();
        }

        public override void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddHeavyVehicle '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0, " +
                $"{VehicleDimensions.Height.ToString(sqlCulture)}, {VehicleDimensions.Weight.ToString(sqlCulture)}, {VehicleDimensions.Length.ToString(sqlCulture)}, 0");

            uint.TryParse(idFromDB, out uint id);
            ID = id;
        }
    }
}
