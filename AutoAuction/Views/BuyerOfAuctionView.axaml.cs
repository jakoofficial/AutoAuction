using AutoAuction.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class BuyerOfAuctionView : UserControl
{
    public BuyerOfAuctionView()
    {
        InitializeComponent();
        DataContext = new BuyerOfAuctionViewModel();
    }
}