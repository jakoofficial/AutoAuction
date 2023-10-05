using AutoAuction.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace AutoAuction.Models.Vehicles
{
    public abstract class Vehicle
    {
        protected Vehicle(
            uint? id,
            string name,
            double km,
            string registrationNumber,
            ushort year,
            decimal newPrice,
            bool hasTowbar,
            double engineSize,
            double kmPerLiter,
            FuelTypeEnum fuelType)
        {
            this.ID = id;
            this.Name = name;
            this.Km = km;
            this.RegistrationNumber = registrationNumber;
            this.Year = year;
            this.NewPrice = newPrice;
            this.HasTowbar = hasTowbar;
            this.EngineSize = engineSize;
            this.KmPerLiter = kmPerLiter;
            this.FuelType = fuelType;
            this._energyClass = GetEnergyClass();
            //TODO: V2 - Add to database and set ID
        }
        protected Vehicle(uint id) 
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetVehicle {id}", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.ID = Convert.ToUInt32(reader.GetInt32(0));
                        this.Name = reader.GetString(1);
                        this.RegistrationNumber = reader.GetString(2);
                        this.Year = (ushort)reader.GetInt32(3);
                        this.NewPrice = reader.GetDecimal(4);
                        this.HasTowbar= reader.GetBoolean(5);
                        this.EngineSize = reader.GetDouble(6);
                        this.KmPerLiter= reader.GetDouble(7);
                        this.FuelType = (FuelTypeEnum)reader.GetInt32(8);
                        this.DriversLicense = (DriversLicenseEnum)reader.GetInt32(9);

                    }
                }
            }
        }
        /// <summary>
        /// ID field and property
        /// </summary>
        public uint? ID { get; protected set; }
        /// <summary>
        /// Name field and property
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Km field and property
        /// </summary>
        public double Km { get; set; }
        /// <summary>
        /// Registration number field and property
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Year field and property
        /// </summary>
        public ushort Year { get; set; }
        /// <summary>
        /// New price field and property
        /// </summary>
        public decimal NewPrice { get; set; }
        /// <summary>
        /// Towbar field and property
        /// </summary>
        public bool HasTowbar { get; set; }
        /// <summary>
        /// Engine size field and property
        /// </summary>
        public virtual double EngineSize { get; set; }
        /// <summary>
        /// Km per liter field and property
        /// </summary>
        public double KmPerLiter { get; set; }
        /// <summary>
        /// Drivers lisence Enum, field and property
        /// </summary>
        public DriversLicenseEnum DriversLicense { get; set; }
        public enum DriversLicenseEnum
        {
            A,
            B,
            C,
            D,
            BE,
            CE,
            DE
        }
        /// <summary>
        /// NFuel type Enum, field and property
        /// </summary>
        public FuelTypeEnum FuelType { get; set; }
        public enum FuelTypeEnum
        {
            Diesel,
            Benzin
        }

        private EnergyClassEnum _energyClass;
        /// <summary>
        /// Engery class Enum, field and property
        /// </summary>
        public EnergyClassEnum EnergyClass
        {
            get { return _energyClass; }
            set
            {
                _energyClass = GetEnergyClass();
            }
        }
        public enum EnergyClassEnum
        {
            A,
            B,
            C,
            D
        }

        protected CultureInfo sqlCulture = new CultureInfo("en-US");
        /// <summary>
        /// Engery class is calculated bassed on year of the car and the efficiancy in km/L.
        /// </summary>
        /// <returns>
        /// Returns the energy class in EnergyClassEnum (A,B,C,D)
        /// </returns>
        private EnergyClassEnum GetEnergyClass()
        {
            if(Year <= 2010)
            {
                if(FuelType == FuelTypeEnum.Benzin)
                {
                    if (KmPerLiter >= 23) return EnergyClassEnum.A;
                    else if (KmPerLiter >= 18 && KmPerLiter < 23) return EnergyClassEnum.B;
                    else if (KmPerLiter >= 13 && KmPerLiter < 18) return EnergyClassEnum.C;
                    else return EnergyClassEnum.D;
                }

                else
                {
                    if(KmPerLiter >= 18) return EnergyClassEnum.A;
                    else if (KmPerLiter >= 14 && KmPerLiter <18) return EnergyClassEnum.B;
                    else if (KmPerLiter >= 10 && KmPerLiter <14) return EnergyClassEnum.C;
                    else return EnergyClassEnum.D;
                }
            }
            else
            {
                if (FuelType == FuelTypeEnum.Benzin)
                {
                    if (KmPerLiter >= 25) return EnergyClassEnum.A;
                    else if (KmPerLiter >= 20  && KmPerLiter <25) return EnergyClassEnum.B;
                    else if (KmPerLiter >= 15 && KmPerLiter <20) return EnergyClassEnum.C;
                    else return EnergyClassEnum.D;
                }

                else
                {
                    if (KmPerLiter >= 20) return EnergyClassEnum.A;
                    else if (KmPerLiter >= 16 && KmPerLiter < 20) return EnergyClassEnum.B;
                    else if (KmPerLiter >= 12 && KmPerLiter < 16) return EnergyClassEnum.C;
                    else return EnergyClassEnum.D;
                }
            }


            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns the vehicle in a string with relivant information.
        /// </summary>
        /// <returns>The Veihcle as a string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ID: {ID}, ");
            sb.Append($"Name: {Name}, ");
            sb.Append($"Km: {Km}, ");
            sb.Append($"Registration Number: {RegistrationNumber}, ");
            sb.Append($"Year: {Year}, ");
            sb.Append($"New price: {NewPrice}, ");
            if (HasTowbar == true)
            {
                sb.Append("Towbar: Yes, ");
            }
            else
            {
                sb.Append("Towbar: No");
            }
            sb.Append($"Engine size: {EngineSize}, ");
            sb.Append($"Km/l: {KmPerLiter}, ");
            sb.Append($"EnergyClass: {EnergyClass}, ");
            sb.Append($"Fuel type: {FuelType.ToString()}, ");
            sb.Append($"Drivers license required: {DriversLicense.ToString()}");
            return sb.ToString();
        }

        public virtual void UploadToDB()
        {
            string idFromDB = Database.Instance.ExecScalar($"EXEC AddVehicle '{Name}', {Km.ToString(sqlCulture)}, '{RegistrationNumber}', {Year}, {NewPrice.ToString(sqlCulture)}, " +
                $"{HasTowbar}, {EngineSize.ToString(sqlCulture)}, {KmPerLiter.ToString(sqlCulture)}, {(int)FuelType}, {(int)DriversLicense}, 0");

            uint.TryParse(idFromDB, out uint id);
            ID = id;
        }
    }
}
