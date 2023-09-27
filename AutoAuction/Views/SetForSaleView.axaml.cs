using AutoAuction.Models.Vehicles;
using AutoAuction.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace AutoAuction.Views;

public partial class SetForSaleView : UserControl
{
    //public List<int> Years { get; } = new();
    public SetForSaleView()
    {
        //for (int i = 1950; i < 2023; i++)
        //{
        //    Years.Add(i);
        //}
        InitializeComponent();
        getVehicleType();
        DataContext = new SetForSaleViewModel();
    }
    private void getVehicleType()
    {
        switch (vehicleTextBlock.Text)
        {
            case "0":
                TruckView truckView = new TruckView();
                vehicleStackPanel.Children.Add(truckView);
                break;
            case "1":
                BusView busView = new BusView();
                vehicleStackPanel.Children.Add(busView);
                break;
            case "2":
                PrivateCarView privCar = new PrivateCarView();
                vehicleStackPanel.Children.Add(privCar);
                break; 
            case "3":
                ProfessionalCarView profCar = new ProfessionalCarView();
                vehicleStackPanel.Children.Add(profCar);
                break;
        }
    }
}