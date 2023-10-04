using Avalonia.Controls;
using AutoAuction.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AutoAuction.Models.Vehicles;
using AutoAuction.Models;
using AutoAuction.DatabaseFiles;
using System.Diagnostics;
using System.Collections.ObjectModel;
using static AutoAuction.Models.Vehicles.Vehicle;
using System.ComponentModel;
using Avalonia.Controls.Primitives;

namespace AutoAuction.ViewModels
{
    public class SetForSaleViewModel : ViewModelBase
    {
        #region View related regions.
        BusViewModel busViewModel = new();
        public BusViewModel BusViewModel
        {
            get => busViewModel;
            set => this.RaiseAndSetIfChanged(ref busViewModel, value);
        }

        TruckViewModel truckViewModel = new();
        public TruckViewModel TruckViewModel
        {
            get => truckViewModel;
            set => this.RaiseAndSetIfChanged(ref truckViewModel, value);

        }

        PrivateCarViewModel privateCarViewModel = new();
        public PrivateCarViewModel PrivateCarViewModel
        {
            get => privateCarViewModel;
            set => this.RaiseAndSetIfChanged(ref privateCarViewModel, value);
        }

        ProfessionalCarViewModel professionalCarViewModel = new();
        public ProfessionalCarViewModel ProfessionalCarViewModel
        {
            get => professionalCarViewModel;
            set => this.RaiseAndSetIfChanged(ref professionalCarViewModel, value);
        }

        ViewModelBase activeView;
        public ViewModelBase ActiveView
        {
            get => activeView;
            set => this.RaiseAndSetIfChanged(ref activeView, value);
        }

        int selectedCarIndex = 0;
        public int SelectedCarIndex
        {
            get => selectedCarIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedCarIndex, value);
                displayVehicleType();
            }
        }

        #endregion
        #region Textboxes
        string txtName;
        public string TxtName
        {
            get => txtName;
            set => this.RaiseAndSetIfChanged(ref txtName, value);
        }

        double txtKilometrage;
        public double TxtKilometrage
        {
            get => txtKilometrage;
            set => this.RaiseAndSetIfChanged(ref txtKilometrage, value);
        }

        string txtRegNumber;
        public string TxtRegNumber
        {
            get => txtRegNumber;
            set => this.RaiseAndSetIfChanged(ref txtRegNumber, value);
        }

        ushort currentYear;
        public ushort CurrentYear
        {
            get => currentYear;
            set => this.RaiseAndSetIfChanged(ref currentYear, value);
        }

        ObservableCollection<ushort> yearList = new ObservableCollection<ushort>();
        public ObservableCollection<ushort> YearList
        {
            get => yearList;
            set => this.RaiseAndSetIfChanged(ref yearList, value);
        }

        double txtKm;
        public double TxtKm
        {
            get => txtKm;
            set => this.RaiseAndSetIfChanged(ref txtKm, value);
        }

        double txtEngineSize;
        public double TxtEngineSize
        {
            get => txtEngineSize;
            set => this.RaiseAndSetIfChanged(ref txtEngineSize, value);
        }

        int selectedFuelType = 0;
        public int SelectedFuelType
        {
            get => selectedFuelType;
            set =>
                this.RaiseAndSetIfChanged(ref selectedFuelType, value);
        }

        bool towbar;
        public bool Towbar
        {
            get => towbar;
            set
            {
                this.RaiseAndSetIfChanged(ref towbar, value);
                driversLicense();
            }
        }

        decimal txtNewPrice;
        public decimal TxtNewPrice
        {
            get => txtNewPrice;
            set => this.RaiseAndSetIfChanged(ref txtNewPrice, value);
        }

        decimal txtStartingBid;
        public decimal TxtStartingBid
        {
            get => txtStartingBid;
            set => this.RaiseAndSetIfChanged(ref txtStartingBid, value);
        }

        DateTime calCloseAuction;
        public DateTime CalCloseAuction
        {
            get => calCloseAuction;
            set => this.RaiseAndSetIfChanged(ref calCloseAuction, value);
        }

        bool noType = false;
        public bool NoType
        {
            get => noType;
            set => this.RaiseAndSetIfChanged(ref noType, value);
        }
        #endregion

        #region Variables

        DriversLicenseEnum drLicense;

        #endregion


        public SetForSaleViewModel()
        {
            //TODO: Check if Towbar works.
            //Towbar = true;
            CbYears();
        }

        void displayVehicleType()
        {
            switch (SelectedCarIndex)
            {
                case 0:
                    ActiveView = null;
                    break;
                case 1:
                    ActiveView = TruckViewModel;
                    break;
                case 2:
                    ActiveView = BusViewModel;
                    break;
                case 3:
                    ActiveView = PrivateCarViewModel;
                    break;
                case 4:
                    ActiveView = ProfessionalCarViewModel;
                    break;
            }
        }

        #region Display Years.
        /// <summary>
        /// Uses current date to get the current Year.
        /// </summary>
        /// <returns>Current year.</returns>
        ushort getCurrentYear()
        {
            DateTime currentDate = DateTime.Now;
            CurrentYear = (ushort)currentDate.Year;
            return CurrentYear;
        }
        /// <summary>
        /// Adds years between Current year, and the creation of Cars, aka. 1886 to YearList (ObservableCollection)
        /// </summary>
        void CbYears()
        {
            CurrentYear = getCurrentYear();
            for (ushort creation = 1886; creation <= CurrentYear; creation++)
            {
                YearList.Add(creation);
            }
        }
        #endregion

        /// <summary>
        /// When Towbar property gets changed, from True / False, sets DriversLicense to CE or C. 
        /// </summary>
        void driversLicense()
        {
            if (Towbar)
            {
                drLicense = DriversLicenseEnum.CE;
            }
            else
            {
                drLicense = DriversLicenseEnum.C;
            }
        }

        #region Methods to create the different Vehicle Types.
        Truck createTruck()
        {
            Truck truck = new Truck(TxtName, TxtKm, TxtRegNumber, CurrentYear, TxtNewPrice, Towbar,
                TxtEngineSize, TxtKilometrage, (Vehicle.FuelTypeEnum)selectedFuelType, (new HeavyVehicle.VehicleDimensionsStruct(TruckViewModel.TxtHeight, TruckViewModel.TxtWeight, TruckViewModel.TxtLength)), 
                drLicense, TruckViewModel.TxtLoadCapacity);
            return truck;
        }
        Bus createBus()
        {
            Bus bus = new Bus (TxtName, TxtKm, TxtRegNumber, CurrentYear, TxtNewPrice, Towbar, TxtEngineSize, TxtKilometrage, (Vehicle.FuelTypeEnum)(selectedFuelType), (new HeavyVehicle.VehicleDimensionsStruct()), drLicense); 
        }


        #endregion
        
        void createVehicle()
        {
            switch (SelectedCarIndex)
            {
                case 0:
                    NoType = true;
                    return;
                case 1:
                    NoType = false;
                    AuctionHouse.SetForSale(createTruck(), Database.Instance.GetUser(User.Instance.UserName), 5M);
                    break;
                case 2:
                    NoType = false;
                    //AuctionHouse.SetForSale()
                    break;
            }
        }
    }
}
