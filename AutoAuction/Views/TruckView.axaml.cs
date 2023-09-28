using Avalonia;
using AutoAuction.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class TruckView : UserControl
{
    public TruckView()
    {
        InitializeComponent();
        DataContext = new TruckViewModel();
    }
}