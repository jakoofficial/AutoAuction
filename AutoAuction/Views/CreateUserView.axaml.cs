using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AutoAuction.ViewModels;

namespace AutoAuction.Views;

public partial class CreateUserView : UserControl
{
    public CreateUserView()
    {
        InitializeComponent();
        DataContext = new CreateUserViewModel();
    }
}