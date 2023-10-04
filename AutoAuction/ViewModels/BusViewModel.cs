using Avalonia.Controls.Documents;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class BusViewModel : ViewModelBase
    {
		private string _height = "";
		public string Height
        {
			get => _height;
			set => this.RaiseAndSetIfChanged(ref _height, value);
		}

        private string _weight = "";
        public string Weight
        {
            get => _weight;
            set => this.RaiseAndSetIfChanged(ref _weight, value);
        }

        private string _length = "";
        public string Length
        {
            get => _length;
            set => this.RaiseAndSetIfChanged(ref _length, value);
        }

        private string _numberOfSeats = "";
        public string NumberOfSeats
        {
            get => _numberOfSeats;
            set => this.RaiseAndSetIfChanged(ref _numberOfSeats, value);
        }

        private string _sleepingSpots = "";
        public string SleepingSpots
        {
            get => _sleepingSpots;
            set => this.RaiseAndSetIfChanged(ref _sleepingSpots, value);
        }

        private bool _hasToilet;
        public bool HasToilet
        {
            get => _hasToilet;
            set => this.RaiseAndSetIfChanged(ref _hasToilet, value);
        }

    }
}
