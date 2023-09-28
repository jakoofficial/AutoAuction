using Avalonia;
using AutoAuction.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class BusView : UserControl
{
    public BusView()
    {
        InitializeComponent();
        DataContext = new BusViewModel();
    }
}