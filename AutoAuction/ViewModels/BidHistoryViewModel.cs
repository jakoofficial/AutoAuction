using AutoAuction.DatabaseFiles;
using AutoAuction.Models;
using AutoAuction.Models.Vehicles;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class BidHistoryViewModel : ViewModelBase
    {
        public string d { get; set; }

        private ObservableCollection<Auction> _bidHistory = new();
        public ObservableCollection<Auction> BidHistory
        {
            get => _bidHistory;
            set => this.RaiseAndSetIfChanged(ref _bidHistory, value);
        }

        public BidHistoryViewModel()
        {
            getBidHistory();
        }

        private void getBidHistory()
        {
            SqlConnection con = new(Database.Instance.ConnectionString);

            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand($"exec GetBidHistory '{User.Instance.UserName}'", con);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BidHistory.Add(new Auction(Auction.GetAuctionVehicle((uint)reader.GetInt32(0)), Database.Instance.GetUser(reader.GetString(1)),
                            Database.Instance.GetUser(reader.GetString(2)), reader.GetDecimal(3), reader.GetDecimal(4), reader.GetBoolean(5)));
                    }
                }
            }
        }

        public void GoBack()
        {
            SetContentArea.Navigate(new HomeScreenViewModel());
        }

    }
}