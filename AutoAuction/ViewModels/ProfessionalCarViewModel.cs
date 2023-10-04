using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class ProfessionalCarViewModel : ViewModelBase
    {
        private string _height = "";
        public string Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        private string _width = "";
        public string Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        private string _depth = "";
        public string Depth
        {
            get => _depth;
            set => this.RaiseAndSetIfChanged(ref _depth, value);
        }

        private string _numberOfSeats = "";
        public string NumberOfSeats
        {
            get => _numberOfSeats;
            set => this.RaiseAndSetIfChanged(ref _numberOfSeats, value);
        }

        private string _loadCapacity = "";
        public string LoadCapacity
        {
            get => _loadCapacity;
            set => this.RaiseAndSetIfChanged(ref _loadCapacity, value);
        }

        private bool _hasSafetybar;
        public bool HasSafetybar
        {
            get => _hasSafetybar;
            set => this.RaiseAndSetIfChanged(ref _hasSafetybar, value);
        }
    }
}
