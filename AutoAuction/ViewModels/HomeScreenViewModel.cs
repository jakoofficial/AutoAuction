using AutoAuction.Models.Vehicles;
using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using AutoAuction.DatabaseFiles;

namespace AutoAuction.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        public string d { get; set; }
        public ObservableCollection<Auction> _dd { get; set; }

        private ObservableCollection<Auction> myAuctions = new();
        public ObservableCollection<Auction> MyAuctions
        {
            get => myAuctions;
            set => this.RaiseAndSetIfChanged(ref myAuctions, value);
        }

        private ObservableCollection<Auction> currentAuctions = new();
        public ObservableCollection<Auction> CurrentAuctions
        {
            get => currentAuctions;
            set => this.RaiseAndSetIfChanged(ref currentAuctions, value);
        }

        public HomeScreenViewModel()
        {
            //ObservableCollection<Auction> tempList = AuctionHouse.GetAllAuctions();

            AuctionHouse.GetAllAuctions();

            foreach (Auction auction in AuctionHouse.Auctions)
            {
                if (auction.Active)
                {
                    CurrentAuctions.Add(auction);
                }
            }

            getMyAuctions();
        }

        private void getMyAuctions()
        {
            if (MyAuctions.Count != 0) { MyAuctions.Clear(); }

            foreach (var auction in AuctionHouse.Auctions)
            {
                if (auction.Seller.UserName == User.Instance.UserName)
                {
                    MyAuctions.Add(auction);
                }
            }
        }

        public void cmdSetForSale()
        {
            SetContentArea.Navigate(new SetForSaleViewModel());
        }
        public void cmdGoToProfile()
        {
            SetContentArea.Navigate(new ProfileViewModel());
        }
        public void cmdGoBidHistory()
        {
            SetContentArea.Navigate(new BidHistoryViewModel());
        }
    }
}