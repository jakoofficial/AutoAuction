using AutoAuction.Models.Vehicles;
using System.Diagnostics;

namespace AutoAuction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        Truck v = new Truck(7);
        //HeavyVehicle hv = new HeavyVehicle(5);
        public MainWindowViewModel()
        {
            Debug.WriteLine(v.ID);
            Debug.WriteLine(v.Name);
            Debug.WriteLine(v.FuelType);
            Debug.WriteLine(v.NewPrice);
            Debug.WriteLine(v.EngineSize);
            Debug.WriteLine(v.KmPerLiter);
            Debug.WriteLine(v.Km);
            Debug.WriteLine(v.DriversLicense);
            Debug.WriteLine(v.EnergyClass);
            Debug.WriteLine(v.RegistrationNumber);
            Debug.WriteLine(v.Year);
            Debug.WriteLine(v.HasTowbar);
            Debug.WriteLine(v.VehicleDimensions);
            Debug.WriteLine(v.LoadCapacity);
        }
    }
}