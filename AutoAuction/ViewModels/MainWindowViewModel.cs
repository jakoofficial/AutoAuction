using AutoAuction.DatabaseFiles;
using AutoAuction.Models;
using AutoAuction.Models.Vehicles;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AutoAuction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool mainWindowEnabled = true;
        public bool MainWindowEnabled
        {
            get => mainWindowEnabled;
            set => this.RaiseAndSetIfChanged(ref mainWindowEnabled, value);
        }
        public int MyProperty { get; set; }
        public MainWindowViewModel()
        {
            //Do not remove(while testing)
            Database.Instance.LogInWithUser("Username", "Password");

        }
    }
}