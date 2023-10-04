using AutoAuction.Models;
using AutoAuction.Views;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class BuyerOfAuctionViewModel : ViewModelBase
    {
        //Instance of WindowManager
        private readonly WindowManager windowManager = new WindowManager();
        #region Prop
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

        //MakeABidTxt 
        string makeABidTxt = "";
        public string MakeABidTxt
        {
            get => makeABidTxt;
            set => this.RaiseAndSetIfChanged(ref makeABidTxt, value);
        }
        #endregion


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
