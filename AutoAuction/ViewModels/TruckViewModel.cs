﻿using System;
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
        //public static Truck CreateTruck(Vehicle v)
        //{
        //    double.TryParse()
        //    Truck t = new Truck("", 0, "", 0, 0, false, 0, 0, Vehicle.FuelTypeEnum.Diesel, 
        //        new HeavyVehicle.VehicleDimensionsStruct((TxtHeight, TxtWeight, TxtLength));

        //    return null;
        //}
    }
}
