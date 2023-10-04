using AutoAuction.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class SellerOfAuctionView : UserControl
{
    public SellerOfAuctionView()
    {
        InitializeComponent();
        DataContext = new SellerOfAuctionViewModel();
    }
}