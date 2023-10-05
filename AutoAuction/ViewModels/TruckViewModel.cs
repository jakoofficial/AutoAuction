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
        double txtHeight;
        public double TxtHeight
        {
            get => txtHeight;
            set => this.RaiseAndSetIfChanged(ref txtHeight, value);
        }
        double txtWeight;
        public double TxtWeight
        {
            get => txtWeight;
            set => this.RaiseAndSetIfChanged(ref txtWeight, value);
        }
        double txtLength;
        public double TxtLength
        {
            get => txtLength;
            set => this.RaiseAndSetIfChanged(ref txtLength, value);
        }
        //public static Truck CreateTruck(Vehicle v)
        //{
        //    double.TryParse()
        //    Truck t = new Truck("", 0, "", 0, 0, false, 0, 0, Vehicle.FuelTypeEnum.Diesel, 
        //        new HeavyVehicle.VehicleDimensionsStruct((TxtHeight, TxtWeight, TxtLength));

        //    return null;
        //}
    }
}
