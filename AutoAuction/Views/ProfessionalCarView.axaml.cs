using Avalonia;
using AutoAuction.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoAuction.Views;

public partial class ProfessionalCarView : UserControl
{
    public ProfessionalCarView()
    {
        InitializeComponent();
        DataContext = new ProfessionalCarViewModel();
    }
}