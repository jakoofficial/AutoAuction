using AutoAuction.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoAuction.Models.Vehicles.HeavyVehicle;

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
            this.DriversLicense = DriversLicenseEnum.B;
        }

        public PersonalCar(uint id) : base(id)
        {
            //TODO: REDO

            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetPersonalCar {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.NumberOfSeat = (ushort)reader.GetInt32(1);
                        this.TrunkDimensions = new TrunkDimensionsStruct(reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4));
                    }
                }
            }
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
            get { return base.EngineSize; }
            set
            {
                if (base.EngineSize < 0.7 && base.EngineSize > 10)
                {
                    throw new ArgumentOutOfRangeException("Engine must be between 0.7 and 10.0 L.");
                }
                base.EngineSize = value;
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

        public override void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddPersonalCar '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0, " +
                $"{NumberOfSeat}, {TrunkDimensions.Height.ToString(sqlCulture)}, {TrunkDimensions.Width.ToString(sqlCulture)}, {TrunkDimensions.Depth.ToString(sqlCulture)}, 0");

            uint.TryParse( idFromDB, out uint id);
            ID = id;
        }
    }
}
