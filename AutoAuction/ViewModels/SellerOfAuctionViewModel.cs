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
        public Auction Auction
        {
            get => auction;
            set => this.RaiseAndSetIfChanged(ref auction, value);
        }

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
        public string vEngeryClass { get; set; }

        public SellerOfAuctionViewModel(){}
        public SellerOfAuctionViewModel(Auction a)
        {
            Auction = a;
            setValues();
        }

        //TODO: Accept function check for date is the day or over the day.

        public void GoBack()
        {
            SetContentArea.Navigate(new HomeScreenViewModel());
        }

        private void setValues()
        {
            string euDate = Auction.EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime usEndDate = DateTime.ParseExact(euDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("da-DK"));

            //Closing = usEndDate.ToString("dd/MM/yyyy");

            Closing = euDate;
            CurrentBid = $"DKK {Auction.StandingBid}";

            vName = "Name: "+ Auction.Vehicle.Name;
            vKm = "Km: " + Auction.Vehicle.Km;
            vYear = "Year: " + Auction.Vehicle.Year.ToString();
            vMinPrice = "Minimum Price: DKK " + Auction.MinimumPrice.ToString();
            vNewPrice = "New Price: DKK " + Auction.Vehicle.NewPrice.ToString();
            vHasTowbar = "Has Towbar: " + Auction.Vehicle.HasTowbar;
            vEngineSize = "Engine Size: " + Auction.Vehicle.EngineSize.ToString();
            vKmPerLiter = "Km Per Liter: " + Auction.Vehicle.KmPerLiter.ToString();
            vFuelType = "Fuel Type: " + Auction.Vehicle.FuelType;
            vDriversLicense = "License Required: " + Auction.Vehicle.DriversLicense;
            //vEngeryClass = "Energy Class: " + Auction.Vehicle.EnergyClass;
        }
    }
}
