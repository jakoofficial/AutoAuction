using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AutoAuction.ViewModels;

namespace AutoAuction.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }
}