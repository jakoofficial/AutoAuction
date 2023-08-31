using System;
using System.Collections.Generic;
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
            DriversLicenseEnum driversLicense,
            bool hasIsofixFittings)
            : base(id, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, numberOfSeat, driversLicense, trunkDimensions)
        {
            //TODO: V19 - PrivatePersonalCar constructor. DriversLicense should be 'B'
            //TODO: V20 - Add to database and set ID
            this.HasIsofixFittings = hasIsofixFittings;
            //throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
