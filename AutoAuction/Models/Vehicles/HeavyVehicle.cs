using AutoAuction.DatabaseFiles;
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
         DriversLicenseEnum driversLicense,
         VehicleDimensionsStruct vehicleDimension) 
         : base(id, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense)
        {
            this.VehicleDimensions = vehicleDimension;
        }
        public HeavyVehicle(uint id) :base(id)
        {
            //TODO: REDO

            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetHeavyVehicle {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //this.ID = Convert.ToUInt32(reader.GetInt32(0));
                        this.Name = reader.GetString(1);
                        this.Km = reader.GetDouble(2);
                        this.RegistrationNumber = reader.GetString(3);
                        this.Year = (ushort)reader.GetInt32(4);
                        this.NewPrice = reader.GetDecimal(5);
                        this.HasTowbar = reader.GetBoolean(6);
                        this.EngineSize = reader.GetDouble(7);
                        this.KmPerLiter = reader.GetDouble(8);
                        this.FuelType = (FuelTypeEnum)reader.GetInt32(9);
                        this.DriversLicense = (DriversLicenseEnum)reader.GetInt32(10);
                        this.VehicleDimensions = new VehicleDimensionsStruct(reader.GetDouble(13), reader.GetDouble(13), reader.GetDouble(13));
                    }
                }
            }

            //this.VehicleDimensions = new VehicleDimensionsStruct();
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
    }
}
