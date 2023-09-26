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

        BusView busView = new BusView();
        vehicleType.Children.Add(busView);

    //DataContext = new SetForSaleViewModel();
    }
}