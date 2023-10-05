using AutoAuction.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public ProfessionalPersonalCar(uint id) : base(id)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetProfessionalPersonalCar {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.LoadCapacity = reader.GetDouble(1);
                        this.HasSafetyBar = reader.GetBoolean(2);
                    }
                }
            }
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

        public override void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddProfessionalPersonalCar '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0, " +
                $"{NumberOfSeat}, {TrunkDimensions.Height.ToString(sqlCulture)}, {TrunkDimensions.Width.ToString(sqlCulture)}, {TrunkDimensions.Depth.ToString(sqlCulture)}, 0, " +
                $"{LoadCapacity.ToString(sqlCulture)}, {HasSafetyBar}, 0");

            uint.TryParse(idFromDB, out uint id);
            ID = id;
        }
    }
}
