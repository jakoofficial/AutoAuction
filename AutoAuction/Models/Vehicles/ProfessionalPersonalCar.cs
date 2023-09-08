using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
    public class ProfessionalPersonalCar : PersonalCar
    {
        public ProfessionalPersonalCar(
            uint id,
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType,
            ushort numberOfSeat,
            DriversLicenseEnum driversLicense,
            TrunkDimensionsStruct trunkDimensions,
            bool hasSafetyBar,
            double loadCapacity)
            : base(id, name, km, registrationNumber, year, newPrice, true, engineSize, kmPerLiter, fuelType, numberOfSeat, driversLicense, trunkDimensions)
        {
            this.HasSafetyBar = hasSafetyBar;
            this.LoadCapacity = loadCapacity;
            if (loadCapacity > 750)
            {
                DriversLicense = DriversLicenseEnum.BE;
            }
            //TODO: V17 - Add to database and set ID
        }

        /// <summary>
        /// Safety Bar 
        /// </summary>
        public bool HasSafetyBar { get; set; }

        /// <summary>
        /// Load Capacity property
        /// </summary>
        public double LoadCapacity { get; set; }

        /// <summary>
        /// Returns the ProfessionalPersonalCar in a string with relevant information.
        /// </summary>
        /// <returns>The Veihcle as a string</returns>
        public override string ToString()
        {
            StringBuilder pCarString = new StringBuilder($"{base.ToString()}, ");
            if (HasSafetyBar == true)
            {
                pCarString.Append("Safety bar: Yes, ");
            }
            else
            {
                pCarString.Append("Safety bar: No, ");
            }
            pCarString.Append($"Load capacity: {string.Format("{0:0.00}", LoadCapacity.ToString())}.");
            return pCarString.ToString();
        }
    }
}
