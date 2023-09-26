using System;
using System.Collections.Generic;
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
            DriversLicenseEnum driversLicense,
            double LoadCapacity) : base(0, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense, vehicleDimensions)
        {
            if (hasTowbar) { this.DriversLicense = DriversLicenseEnum.CE; }
            else { this.DriversLicense = DriversLicenseEnum.C; }
            this.VehicleDimensions = vehicleDimensions;
            this.LoadCapacity = LoadCapacity;
            //TODO: V10 - Constructor for Truck, DriversLisence should be CE if the truck has a towbar, otherwise it should be C
            //TODO: V11 - Add to database and set ID
        }
        public Truck(uint id) : base(id)
        {
            //TODO: REDO
            this.LoadCapacity = 15;
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
        /// Returns the Truck in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            StringBuilder tsb = new StringBuilder($"{base.ToString()}, ");
            tsb.Append($"Load Capacity: {this.LoadCapacity}");
            //TODO: V12 - ToString for Truck
            return tsb.ToString();
        }
    }
}
