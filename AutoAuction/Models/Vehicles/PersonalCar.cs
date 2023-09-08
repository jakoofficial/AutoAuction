using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
    public abstract class PersonalCar : Vehicle
    {
        protected PersonalCar(
            //TODO: V14 Initialisering
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
            DriversLicenseEnum driversLicense,
            TrunkDimensionsStruct trunkDimensions)
            : base(id, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense)
        {
            this.NumberOfSeat = numberOfSeat;
            this.TrunkDimensions = trunkDimensions;
        }
        /// <summary>
        /// Number of seat property
        /// </summary>
        public ushort NumberOfSeat { get; set; }
        /// <summary>
        /// Trunk dimentions property and struct
        /// </summary>
        public TrunkDimensionsStruct TrunkDimensions { get; set; }
        public readonly struct TrunkDimensionsStruct
        {
            public TrunkDimensionsStruct(double height, double width, double depth)
            {
                Height = height;
                Width = width;
                Depth = depth;
            }
            public double Height { get; }
            public double Width { get; }
            public double Depth { get; }
            public override string ToString() => $"(Height: {Height}, Width: {Width}, Depth: {Depth})";
        }
        /// <summary>
        /// Engine size property
        /// must be between 0.7 and 10.0 L or cast an out of range exection.
        /// </summary>
        public override double EngineSize
        {
            get { return EngineSize; }
            set
            {
                if (EngineSize < 0.7 && EngineSize > 10)
                {
                    throw new ArgumentOutOfRangeException("Engine must be between 0.7 and 10.0 L.");
                }
                EngineSize = value;
            }
        }
        /// <summary>
        /// Returns the PersonalCar in a string with relivant information.
        /// </summary>
        public override string ToString()
        {
            StringBuilder pCarString = new StringBuilder($"{base.ToString()}, ");
            pCarString.Append($"Seats: {NumberOfSeat}, ");
            pCarString.Append($"Trunk Dimensions: {TrunkDimensions.ToString()}, ");
            return pCarString.ToString();
        }
    }
}
