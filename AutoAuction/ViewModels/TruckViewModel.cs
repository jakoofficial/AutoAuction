using System;
using AutoAuction.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoAuction.Models.Vehicles;
using ReactiveUI;

namespace AutoAuction.ViewModels
{
    public class TruckViewModel : ViewModelBase
    {
        string txtHeight;
        public string TxtHeight
        {
            get => txtHeight;
            set => this.RaiseAndSetIfChanged(ref txtHeight, value);
        }
        string txtWeight;
        public string TxtWeight
        {
            get => txtWeight;
            set => this.RaiseAndSetIfChanged(ref txtWeight, value);
        }
        string txtLength;
        public string TxtLength
        {
            get => txtLength;
            set => this.RaiseAndSetIfChanged(ref txtLength, value);
        }

        string txtLoadCapacity;
        public string TxtLoadCapacity
        {
            get => txtLoadCapacity;
            set => this.RaiseAndSetIfChanged(ref txtLoadCapacity, value);
        }
    }
}
