using AutoAuction.Models;
using AutoAuction.Models.Vehicles;
using Microsoft.VisualBasic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class SellerOfAuctionViewModel : ViewModelBase
    {
        //Closing Txt
        string closing = "##/##/##";
        public string Closing
        {
            get => closing;
            set => this.RaiseAndSetIfChanged(ref closing, value);
        }
        //CurrentBid Txt
        string currentBid = "DKK ###.###";
        public string CurrentBid
        {
            get => currentBid;
            set => this.RaiseAndSetIfChanged(ref currentBid, value);
        }

        private Auction auction;
        public Auction Auction { get; set; }

        public string vName { get; set; }
        public string vRegNumber { get; set; }
        public string vKm { get; set; }
        public string vYear { get; set; }
        public string vMinPrice { get; set; }
        public string vNewPrice { get; set; }
        public string vHasTowbar { get; set; }
        public string vEngineSize { get; set; }
        public string vKmPerLiter { get; set; }
        public string vFuelType { get; set; }
        public string vDriversLicense { get; set; }
        public string vEnergyClass { get; set; }
        public string vDimension { get; set; }
        public double vLoadCapacity { get; set; }
        public string vTrunkDimension { get; set; }

        //Vehicle Types
        //Truck
        public bool TruckAuctionVis { get; set; }

        //Bus
        public bool BusAuctionVis { get; set; }
        public ushort vSeats { get; set; }
        public ushort vSleepSpaces { get; set; }
        public bool vToilet { get; set; }

        //PrivateCar
        public bool PrivateCarAuctionVis { get; set; }
        public bool vIsofix { get; set; }
        //ProfCar
        public bool ProfCarAuctionVis { get; set; }
        public bool vSafetyBar { get; set; }
        public string VehicleType { get; set; }


        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => this.RaiseAndSetIfChanged(ref isActive, value);
        }

        public SellerOfAuctionViewModel() { }
        public SellerOfAuctionViewModel(Auction a)
        {
            Auction = a;
            setValues();
        }

        //TODO: Accept function check for date is the day or over the day.
        public void EndAuction()
        {
            Auction a = this.Auction;
            AuctionHouse.UpdateAuction(Auction.Buyer.UserName,Auction.ID, Auction.StandingBid, false);
            AuctionHouse.Auctions.Clear();
            AuctionHouse.GetAllAuctions();
            IsActive = false;

            //SetContentArea.Navigate(new HomeScreenViewModel());
        }

        public void GoBack()
        {
            SetContentArea.Navigate(new HomeScreenViewModel());
        }

        private void setValues()
        {
            string euDate = Auction.EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime usEndDate = DateTime.ParseExact(euDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("da-DK"));

            Vehicle v = Auction.GetAuctionVehicle((uint)Auction.Vehicle.ID);
            if (v != null)
            {
                //Closing = usEndDate.ToString("dd/MM/yyyy");
                Closing = euDate;
                CurrentBid = $"DKK {Auction.StandingBid}";

                vName = "Name: " + Auction.Vehicle.Name.ToString();
                vKm = "Km: " + Auction.Vehicle.Km;
                vYear = "Year: " + Auction.Vehicle.Year.ToString();
                vMinPrice = "Minimum Price: DKK " + Auction.MinimumPrice.ToString();
                vNewPrice = "New Price: DKK " + Auction.Vehicle.NewPrice.ToString();
                vHasTowbar = "Has Towbar: " + Auction.Vehicle.HasTowbar;
                vEngineSize = "Engine Size: " + Auction.Vehicle.EngineSize.ToString();
                vKmPerLiter = "Km Per Liter: " + Auction.Vehicle.KmPerLiter.ToString();
                vFuelType = "Fuel Type: " + Auction.Vehicle.FuelType;
                vDriversLicense = "License Required: " + Auction.Vehicle.DriversLicense;
                vEnergyClass = "Energy Class: " + Auction.Vehicle.EnergyClass.ToString();

                if (DateTime.Compare(DateTime.Now, Auction.EndDate) < 0 || !Auction.Active)
                {
                    IsActive = false;
                }
                else
                {
                    IsActive = true;
                }
                
                if (Auction.GetAuctionVehicle((uint)Auction.Vehicle.ID) is Bus)
                {
                    Bus b = (Bus)Auction.Vehicle;
                    vSeats = b.NumberOfSeats;
                    vSleepSpaces = b.NumberOfSleepingSpaces;
                    vToilet = b.HasToilet;
                    VehicleType = "Bus";
                    setElementVisibility("Bus");
                    return;
                }
                if (Auction.GetAuctionVehicle((uint)Auction.Vehicle.ID) is Truck)
                {
                    Truck t = (Truck)Auction.Vehicle;
                    vLoadCapacity = 5;
                    vDimension = $"Height: {t.VehicleDimensions.Height}, Lenght:{t.VehicleDimensions.Length}, Weight: {t.VehicleDimensions.Weight}";
                    VehicleType = "Truck";
                    setElementVisibility("Truck");
                    return;
                }
                if (Auction.GetAuctionVehicle((uint)Auction.Vehicle.ID) is PrivatePersonalCar)
                {
                    PrivatePersonalCar p = (PrivatePersonalCar)Auction.Vehicle;
                    vSeats = p.NumberOfSeat;
                    vTrunkDimension = $"Height: {p.TrunkDimensions.Height}, Depth:{p.TrunkDimensions.Depth}, Width: {p.TrunkDimensions.Width}";
                    vIsofix = p.HasIsofixFittings;
                    VehicleType = "Private car";
                    setElementVisibility("PrivCar");
                    return;
                }
                if (Auction.GetAuctionVehicle((uint)Auction.Vehicle.ID) is ProfessionalPersonalCar)
                {
                    ProfessionalPersonalCar p = (ProfessionalPersonalCar)Auction.Vehicle;
                    vSeats = p.NumberOfSeat;
                    vLoadCapacity = p.LoadCapacity;
                    vSafetyBar = p.HasSafetyBar;
                    vTrunkDimension = $"Height: {p.TrunkDimensions.Height}, Depth:{p.TrunkDimensions.Depth}, Width: {p.TrunkDimensions.Width}";
                    VehicleType = "Professional car";
                    setElementVisibility("ProfCar");
                    return;
                }

            }

            void setElementVisibility(string visible)
            {
                switch (visible)
                {
                    case "Truck": TruckAuctionVis = true;
                        BusAuctionVis = false;
                        PrivateCarAuctionVis = false;
                        ProfCarAuctionVis = false;
                        break;
                    case "Bus": BusAuctionVis = true;
                        TruckAuctionVis = false;
                        PrivateCarAuctionVis = false;
                        ProfCarAuctionVis = false;
                        break;
                    case "PrivCar": PrivateCarAuctionVis = true;
                        TruckAuctionVis = false;
                        BusAuctionVis = false;
                        ProfCarAuctionVis = false;
                        break;
                    case "ProfCar": ProfCarAuctionVis = true;
                        TruckAuctionVis = false;
                        BusAuctionVis = false;
                        PrivateCarAuctionVis = false;
                        break;
                }
            }

        }
    }
}
