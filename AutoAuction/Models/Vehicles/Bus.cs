using AutoAuction.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models.Vehicles
{
   public class Bus : HeavyVehicle
    {
        public Bus(
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
            bool hasToilet) : base(0, name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense, vehicleDimension)
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
        }

        public Bus (uint id) : base(id)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetBus {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.HasToilet = reader.GetBoolean(0);
                        this.NumberOfSeats = (ushort)reader.GetInt32(1);
                        this.NumberOfSleepingSpaces = (ushort)reader.GetInt32(2);
                    }
                }
            }
        }

        /// <summary>
        /// Engine size 
        /// 
        /// must be between 4.2 and 15.0 L or cast an out of range exection.
        /// </summary>
        public override double EngineSize
        {
            get { return base.EngineSize; }
            set
            {
                if (EngineSize < 4.2 && EngineSize > 15)
                {
                    throw new ArgumentOutOfRangeException();
                }
                //V7 - TODO value must be between 4.2 and 15.0 L or cast an out of range exection.
                base.EngineSize = value;
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
            return dsb.ToString();
        }

        public override void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddBus '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0, " +
                $"{VehicleDimensions.Height.ToString(sqlCulture)}, {VehicleDimensions.Weight.ToString(sqlCulture)}, {VehicleDimensions.Length.ToString(sqlCulture)}, 0, " +
                $"{NumberOfSeats}, {NumberOfSleepingSpaces}, {HasToilet}, 0");

            uint.TryParse(idFromDB, out uint id);
            ID = id;
        }
    }
}
