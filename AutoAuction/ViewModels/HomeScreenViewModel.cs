using AutoAuction.Models.Vehicles;
using AutoAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace AutoAuction.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        public string d { get; set; }
        public ObservableCollection<Auction> _dd { get; set; }

        private ObservableCollection<Auction> ddd;
        public ObservableCollection<Auction> test
        {
            get => ddd;
            set => this.RaiseAndSetIfChanged(ref ddd, value);
        }
        public HomeScreenViewModel()
        {
            //Auction n = new Auction(new Bus("Ooga", 20, "regiNR", 2010, 203212, false, 3232, 2312, Vehicle.FuelTypeEnum.Diesel,
            //   new HeavyVehicle.VehicleDimensionsStruct(20, 20, 20), Vehicle.DriversLicenseEnum.B, 2, 1, true),
            //   new PrivateUser("User", "woaaaaah", 1029, 3389992827), 231);

            //_d = new ObservableCollection<Auction>();
            //_d.Add(n);
        }

    }
}