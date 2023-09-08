using AutoAuction.Models.Vehicles;

namespace AutoAuction.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ProfessionalPersonalCar pf = new ProfessionalPersonalCar(1, "Bilbil", 2, "ssssssss", 2023, 203221, 2312, 2031, Vehicle.FuelTypeEnum.Benzin, 3, Vehicle.DriversLicenseEnum.B, new PersonalCar.TrunkDimensionsStruct(20,20,20), false, 2);
        public string Greeting => pf.ToString();
    }
}