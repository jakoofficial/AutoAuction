using AutoAuction.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class ProfileView : UserControl
{
    public ProfileView()
    {
        DataContext = new ProfileViewModel();
        InitializeComponent();
    }
}