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
using System.ComponentModel;
using Avalonia.Controls.Primitives;

namespace AutoAuction.ViewModels
{
    public class SetForSaleViewModel : ViewModelBase
    {
        /// <summary>
        /// The Views in the region below, are used to display the specific properties, each object type needs.
        /// Ex. Truck has the Truck Dimensions, that include Height, Weight and Length. Truck also contains Load Capacity.
        /// </summary>
        #region View related regions.


        BusViewModel busViewModel = new();
        public BusViewModel BusVM
        {
            get => busViewModel;
            set => this.RaiseAndSetIfChanged(ref busViewModel, value);
        }

        TruckViewModel truckViewModel = new();
        public TruckViewModel TruckVM
        {
            get => truckViewModel;
            set => this.RaiseAndSetIfChanged(ref truckViewModel, value);

        }

        PrivateCarViewModel privateCarViewModel = new();
        public PrivateCarViewModel PrivateCarVM
        {
            get => privateCarViewModel;
            set => this.RaiseAndSetIfChanged(ref privateCarViewModel, value);
        }

        ProfessionalCarViewModel professionalCarViewModel = new();
        public ProfessionalCarViewModel ProfessionalCarVM
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

        /// <summary>
        /// Region below contains full properties, that are bound to a TextBox, ComboBox, RadioButton or Calendar in the View.
        /// </summary>
        #region Textboxes
        string txtName;
        public string TxtName
        {
            get => txtName;
            set => this.RaiseAndSetIfChanged(ref txtName, value);
        }

        string txtKilometrage;
        public string TxtKilometrage
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

        string txtKm;
        public string TxtKm
        {
            get => txtKm;
            set => this.RaiseAndSetIfChanged(ref txtKm, value);
        }

        string txtEngineSize;
        public string TxtEngineSize
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
            set => this.RaiseAndSetIfChanged(ref towbar, value);
        }

        string txtNewPrice;
        public string TxtNewPrice
        {
            get => txtNewPrice;
            set => this.RaiseAndSetIfChanged(ref txtNewPrice, value);
        }

        string txtStartingBid;
        public string TxtStartingBid
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

        bool creationError = false;
        public bool CreationError
        {
            get => creationError;
            set => this.RaiseAndSetIfChanged(ref creationError, value);
        }

        DateTime closeAuctionTime = DateTime.Now;
        public DateTime CloseAuctionTime
        {
            get => closeAuctionTime;
            set => this.RaiseAndSetIfChanged(ref closeAuctionTime, value);
        }
        #endregion


        public SetForSaleViewModel()
        {
            //TODO: Check if Towbar works.
            //Towbar = true;
            CbYears();
        }

        /// <summary>
        /// Sets ActiveView, to another ViewModel depending on which Type of Vehicle, the user is creating. 
        /// </summary>
        void displayVehicleType()
        {
            switch (SelectedCarIndex)
            {
                case 0:
                    ActiveView = null;
                    break;
                case 1:
                    ActiveView = TruckVM;
                    break;
                case 2:
                    ActiveView = BusVM;
                    break;
                case 3:
                    ActiveView = PrivateCarVM;
                    break;
                case 4:
                    ActiveView = ProfessionalCarVM;
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

        #region Methods to create the different Vehicle Types.
        Truck createTruck()
        {
            Truck truck = new Truck(TxtName, Convert.ToDouble(TxtKm), TxtRegNumber, CurrentYear, Convert.ToDecimal(TxtNewPrice), Towbar, Convert.ToDouble(TxtEngineSize), Convert.ToDouble(TxtKilometrage), (Vehicle.FuelTypeEnum)selectedFuelType,
                            (new HeavyVehicle.VehicleDimensionsStruct(Convert.ToDouble(TruckVM.TxtHeight), Convert.ToDouble(TruckVM.TxtWeight), Convert.ToDouble(TruckVM.TxtLength))),
                            Convert.ToDouble(TruckVM.TxtLoadCapacity));
            return truck;

        }
        Bus createBus()
        {
            Bus bus = new Bus(TxtName, Convert.ToDouble(TxtKm), TxtRegNumber, CurrentYear, Convert.ToDecimal(TxtNewPrice), Towbar, Convert.ToDouble(TxtEngineSize), Convert.ToDouble(TxtKilometrage),
                (Vehicle.FuelTypeEnum)(selectedFuelType), (new HeavyVehicle.VehicleDimensionsStruct(Convert.ToDouble(BusVM.Height), Convert.ToDouble(BusVM.Weight), Convert.ToDouble(BusVM.Length))),
                Convert.ToUInt16(BusVM.NumberOfSeats), (ushort)Convert.ToInt16(BusVM.SleepingSpots), BusVM.HasToilet);
            return bus;
        }
        PrivatePersonalCar createPrivatePersonalCar()
        {
            PrivatePersonalCar privCar = new PrivatePersonalCar(0, TxtName, Convert.ToDouble(TxtKm), TxtRegNumber, CurrentYear, Convert.ToDecimal(TxtNewPrice), Towbar, Convert.ToDouble(TxtEngineSize), Convert.ToDouble(TxtKilometrage),
                (Vehicle.FuelTypeEnum)(selectedFuelType), Convert.ToUInt16(PrivateCarVM.NumberOfSeats), (new PersonalCar.TrunkDimensionsStruct(Convert.ToDouble(PrivateCarVM.Height), Convert.ToDouble(PrivateCarVM.Width), Convert.ToDouble(PrivateCarVM.Depth))), PrivateCarVM.HasIsofix);
            return privCar;
        }
        ProfessionalPersonalCar createProfessionalPersonalCar()
        {
            ProfessionalPersonalCar proCar = new ProfessionalPersonalCar(0, TxtName, Convert.ToDouble(TxtKm), TxtRegNumber, CurrentYear, Convert.ToDecimal(TxtNewPrice), Convert.ToDouble(TxtEngineSize), Convert.ToDouble(TxtKilometrage),
                (Vehicle.FuelTypeEnum)(selectedFuelType), Convert.ToUInt16(ProfessionalCarVM.NumberOfSeats), (new PersonalCar.TrunkDimensionsStruct(Convert.ToDouble(ProfessionalCarVM.Height), Convert.ToDouble(ProfessionalCarVM.Width), Convert.ToDouble(ProfessionalCarVM.Depth))), ProfessionalCarVM.HasSafetybar, Convert.ToDouble(ProfessionalCarVM.LoadCapacity));

            return proCar;
        }

        #endregion

        public void CreateVehicle()
        {
            switch (SelectedCarIndex)
            {
                case 0:
                    NoType = true;
                    return;
                case 1:
                    NoType = false;
                    try
                    {
                        Truck t = createTruck();
                        t.UploadToDB();
                        AuctionHouse.SetForSale(t, Database.Instance.GetUser(User.Instance.UserName), Convert.ToDecimal(TxtStartingBid), CloseAuctionTime);
                        CreationError = false;
                    }
                    catch (Exception)
                    {
                        CreationError = true;
                        return;
                    }
                    break;
                case 2:
                    NoType = false;
                    try
                    {
                        Bus b = createBus();
                        b.UploadToDB();
                        AuctionHouse.SetForSale(b, Database.Instance.GetUser(User.Instance.UserName), Convert.ToDecimal(TxtStartingBid), CloseAuctionTime);
                        CreationError = false;

                    }
                    catch (Exception)
                    {
                        CreationError = true;
                        return;
                    }
                    break;
                case 3:
                    NoType = false;
                    try
                    {
                        PrivatePersonalCar privCar = createPrivatePersonalCar();
                        privCar.UploadToDB();
                        AuctionHouse.SetForSale(privCar, Database.Instance.GetUser(User.Instance.UserName), Convert.ToDecimal(TxtStartingBid), CloseAuctionTime);
                        CreationError = false;
                    }
                    catch (Exception)
                    {
                        CreationError = true;
                        return;
                    }
                    break;
                case 4:
                    NoType = false;
                    try
                    {
                        ProfessionalPersonalCar proCar = createProfessionalPersonalCar();
                        proCar.UploadToDB();
                        AuctionHouse.SetForSale(proCar, Database.Instance.GetUser(User.Instance.UserName), Convert.ToDecimal(TxtStartingBid), CloseAuctionTime);
                        CreationError = false;
                    }
                    catch (Exception)
                    {
                        CreationError = true;
                        return;
                    }
                    break;
            }
        }

        public void CancelBtn()
        {
            SetContentArea.Navigate(new HomeScreenViewModel());
        }
    }
}
