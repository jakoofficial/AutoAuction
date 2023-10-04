using Avalonia;
using AutoAuction.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reactive.Concurrency;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoAuction.Views;
using AutoAuction.Models;

namespace AutoAuction;

public partial class MakeABidView : Window
{
    public MakeABidView(WindowManager wm)
    {
        InitializeComponent();
        MakeABidViewModel viewModel = new MakeABidViewModel(wm);
        DataContext = viewModel;
    }
}