using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class PrivateCarViewModel : ViewModelBase
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

        private bool _hasIsofix;
        public bool HasIsofix
        {
            get => _hasIsofix;
            set => this.RaiseAndSetIfChanged(ref _hasIsofix, value);
        }
    }
}
