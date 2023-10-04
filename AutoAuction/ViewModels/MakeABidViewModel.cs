using AutoAuction.Models;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class MakeABidViewModel : ReactiveObject
    {
        private readonly WindowManager _windowManager;

        public ReactiveCommand<Unit, Unit> CloseWindowCommand { get; }

        public MakeABidViewModel(WindowManager windowManager)
        {
            _windowManager = windowManager;

            CloseWindowCommand = ReactiveCommand.Create(CancelBid);
        }
        public void CancelBid()
        {
            if (_windowManager.IsWindowOpen<MakeABidView>())
            {
                var window = _windowManager.GetOpenWindow<MakeABidView>();
                if (window != null)
                {
                    window.Close();
                }
            }
        }
    }
}
