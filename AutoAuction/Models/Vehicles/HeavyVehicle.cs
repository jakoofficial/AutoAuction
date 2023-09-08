using System;
using System.Collections.Generic;
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
