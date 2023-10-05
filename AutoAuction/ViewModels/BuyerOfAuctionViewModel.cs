using AutoAuction.DatabaseFiles;
using AutoAuction.Interfaces;
using AutoAuction.Models;
using AutoAuction.Models.Vehicles;
using AutoAuction.Views;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class BuyerOfAuctionViewModel : ViewModelBase
    {
        //Instance of WindowManager
        private readonly WindowManager windowManager = new WindowManager();
        #region Prop

        string closingDate = "";
        public string ClosingDate
        {
            get => closingDate;
            set => this.RaiseAndSetIfChanged(ref closingDate, value);
        }
        //CurrentBid Txt
        string currentBid = "";
        public string CurrentBid
        {
            get => currentBid;
            set => this.RaiseAndSetIfChanged(ref currentBid, value);
        }

        //MakeABidTxt 
        string makeABidTxt = "";
        public string MakeABidTxt
        {
            get => makeABidTxt;
            set => this.RaiseAndSetIfChanged(ref makeABidTxt, value);
        }

        string reloadPriceTxt = "";
        public string ReloadPriceTxt
        {
            get => reloadPriceTxt;
            set => this.RaiseAndSetIfChanged(ref reloadPriceTxt, value);
        }

        public Auction CAuction { get; set; }
        private uint ID { get; set; }
        public string MinimumPrice { get; set; }
        public string StandingBid { get; set; }
        public bool Active { get; set; }
        public DateTime EndDate { get; set; }
        internal ISeller Seller { get; set; }
        internal IBuyer? Buyer { get; set; }

        #endregion

        public BuyerOfAuctionViewModel()
        {

        }

        public BuyerOfAuctionViewModel(Auction a)
        {
            CAuction = a;
            setValues();
        }
        public void UpdateCurrentBid()
        {
            string s = Database.Instance.ExecScalar($"exec GetAuctionPrice {ID}");
            if (!string.IsNullOrEmpty(s))
            {
                ReloadPriceTxt = "Updated Price.";
            }
            else
            {
                ReloadPriceTxt = "Failed to update.";
            }
        }

        private void setValues()
        {
            ID = CAuction.ID;
            MinimumPrice = CAuction.MinimumPrice.ToString();
            StandingBid = CAuction.StandingBid.ToString();
            Active = CAuction.Active;
            EndDate = CAuction.EndDate;
            Seller = CAuction.Seller;
            Buyer = CAuction.Buyer;

            string euDate = EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ClosingDate = euDate;

            CurrentBid = $"DKK {StandingBid}";
        }

        public async Task MakeABidBtn()
        {
            if (!windowManager.IsWindowOpen<MakeABidView>())
            {
                MakeABidTxt = "";
                var window = new MakeABidView(windowManager);
                windowManager.ShowWindow(window);
            }
            else { MakeABidTxt = "Window Is Already Opened"; }
        }
    }
}
