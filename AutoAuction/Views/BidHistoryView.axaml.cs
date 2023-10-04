using AutoAuction.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class BidHistoryView : UserControl
{
    public BidHistoryView()
    {
        InitializeComponent();
        DataContext = new BidHistoryViewModel();
    }
}