using Avalonia.Controls;
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
        int selectedCarIndex = 0;
        public int SelectedCarIndex
        {
            get => selectedCarIndex;
            set => this.RaiseAndSetIfChanged(ref selectedCarIndex, value);
        }
        public SetForSaleViewModel()
        {
            getVehicleType();
        }

        private void getVehicleType()
        {
            switch (SelectedCarIndex)
            {
                case 1:
                    TruckView truckView = new TruckView();
                    //vehicleStackPanel.Children.Add(truckView);
                    break;
                case 2:
                    BusView busView = new BusView();
                    //vehicleStackPanel.Children.Add(busView);
                    break;
                case 3:
                    PrivateCarView privCar = new PrivateCarView();
                    //vehicleStackPanel.Children.Add(privCar);
                    break;
                case 4:
                    ProfessionalCarView profCar = new ProfessionalCarView();
                    //vehicleStackPanel.Children.Add(profCar);
                    break;
            }
        }

    }
}
