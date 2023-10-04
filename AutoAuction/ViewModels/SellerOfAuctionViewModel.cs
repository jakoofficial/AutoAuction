using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class SellerOfAuctionViewModel : ViewModelBase
    {
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
        #endregion
    }
}
