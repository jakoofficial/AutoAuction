using Avalonia.Controls;
using AutoAuction.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AutoAuction.ViewModels
{
    public class SetForSaleViewModel : ViewModelBase
    {
        #region Properties
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

        bool showProfessional;
        public bool ShowProfessional
        {
            get => showProfessional;
            set => this.RaiseAndSetIfChanged(ref showProfessional, value);
        }

        bool showPrivate;
        public bool ShowPrivate
        {
            get => showPrivate;
            set => this.RaiseAndSetIfChanged(ref showPrivate, value);
        }

        bool showTruck;
        public bool ShowTruck
        {
            get => showTruck;
            set => this.RaiseAndSetIfChanged(ref showTruck, value);
        }

        bool showBus;
        public bool ShowBus
        {
            get => showBus;
            set => this.RaiseAndSetIfChanged(ref showBus, value);
        }

        int selectedCarIndex = 0;
        public int SelectedCarIndex
        {
            get => selectedCarIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedCarIndex, value);
                getVehicleType();
            }
        }

        ViewModelBase activeView;
        public ViewModelBase ActiveView
        {
            get => activeView;
            set => this.RaiseAndSetIfChanged(ref activeView, value);
        }

        #endregion

        public SetForSaleViewModel()
        {
        }

        private void getVehicleType()
        {
            switch (SelectedCarIndex)
            {
                case 0:
                    ActiveView = TruckViewModel;
                    break;
                case 1:
                    ActiveView = BusViewModel;
                    break;
                case 2:
                    ActiveView = PrivateCarViewModel;
                    break;
                case 3:
                    ActiveView = ProfessionalCarViewModel;
                    break;
            }
        }
    }
}
