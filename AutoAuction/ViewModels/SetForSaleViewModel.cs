using Avalonia.Controls;
using AutoAuction.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class SetForSaleViewModel : ViewModelBase
    {
        BusViewModel busViewModel= new();
        BusViewModel BusViewModel 
        {
            get => busViewModel; 
            set => this.RaiseAndSetIfChanged(ref busViewModel, value);
        }

        TruckViewModel truckViewModel = new();
        TruckViewModel TruckViewModel 
        {
            get => truckViewModel;
            set => this.RaiseAndSetIfChanged(ref truckViewModel, value);

        }
        PrivateCarViewModel privateCarViewModel = new();
        PrivateCarViewModel PrivateCarViewModel 
        {
            get => privateCarViewModel;
            set => this.RaiseAndSetIfChanged(ref privateCarViewModel, value);
        }

        ProfessionalCarViewModel professionalCarViewModel = new();
        ProfessionalCarViewModel ProfessionalCarViewModel
        {
            get => professionalCarViewModel;
            set => this.RaiseAndSetIfChanged(ref professionalCarViewModel, value);
        }

        ViewModelBase activeView;
        ViewModelBase ActiveView 
        {
            get => activeView;
            set => this.RaiseAndSetIfChanged(ref activeView, value);
        }

        int selectedCarIndex = 0;
        public int SelectedCarIndex
        {
            get => selectedCarIndex;
            set {
                this.RaiseAndSetIfChanged(ref selectedCarIndex, value);
                getVehicleType();
            }
        }
        public SetForSaleViewModel()
        {
            //ActiveView = TruckViewModel;
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
