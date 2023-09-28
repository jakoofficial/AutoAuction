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
        BusView busvm = new();
        BusView BusViewModel 
        {
            get => busvm; 
            set => this.RaiseAndSetIfChanged(ref busvm, value);
        }
        TruckView TruckViewModel { get; set; } = new();
        PrivateCarView PrivateCarViewModel { get; set; } = new();

        ProfessionalCarViewModel ProfessionalCarViewModel { get; set; }

        UserControl activeView;
        UserControl ActiveView 
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
            ActiveView = TruckViewModel;
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
                   // ActiveView = ProfessionalCarViewModel;
                    break;
            }
        }
    }
}
