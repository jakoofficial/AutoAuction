using AutoAuction.DatabaseFiles;
using AutoAuction.Models.Vehicles;
using ReactiveUI;
using System;
using System.Diagnostics;

namespace AutoAuction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            //Do not remove(while testing)
            Database.Instance.LogInWithUser("Username" , "Password");

        }
    }
}