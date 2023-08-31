using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
   public class Bus : HeavyVehicle
    {
        public Bus(
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
            VehicleDimensionsStruct vehicleDimension,
            DriversLicenseEnum driversLicense,
            ushort numberOfSeats,
            ushort numberOfSleepingSpaces,
            bool hasToilet) : base(id, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense, vehicleDimension)
        {
            if (HasTowbar)
            {
                this.DriversLicense = DriversLicenseEnum.DE;
            }
            else
            {
                this.DriversLicense = DriversLicenseEnum.D;
            }
            this.NumberOfSeats = numberOfSeats;
            this.NumberOfSleepingSpaces = numberOfSleepingSpaces;
            this.HasToilet = hasToilet;
            //TODO: V7 - set contructor and DriversLisence to DE if the car has a towbar or D if not.
            //TODO: V8 - Add to database and set ID
            throw new NotImplementedException();
        }
        /// <summary>
        /// Engine size 
        /// 
        /// must be between 4.2 and 15.0 L or cast an out of range exection.
        /// </summary>
        public override double EngineSize
        {
            get { return EngineSize; }
            set
            {
                if (EngineSize < 4.2 || EngineSize > 15)
                {
                    throw new ArgumentOutOfRangeException();
                }
                //V7 - TODO value must be between 4.2 and 15.0 L or cast an out of range exection.
                EngineSize = value;
            }
        }
        /// <summary>
        /// NumberOfSeats property
        /// </summary>
        public ushort NumberOfSeats { get; set; }
        /// <summary>
        /// NumberOfSeats property
        /// </summary>
        public ushort NumberOfSleepingSpaces { get; set; }
        /// Towbar property
        /// </summary>
        public bool HasToilet { get; set; }
        /// <summary>
        /// Returns the Bus in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            StringBuilder dsb = new StringBuilder($"{base.ToString()}, ");
            dsb.Append($"Number of seats: {NumberOfSeats}, ");
            dsb.Append($"Number of sleeping spaces: {NumberOfSleepingSpaces}, ");
            if (HasToilet)
            {
                dsb.Append($"Toilet: Yes, ");
            }
            else
            {
                dsb.Append($"Toilet: No, ");
            }
            //TODO: V9 - Tostring for bus
            throw new NotImplementedException();
        }
    }
}
