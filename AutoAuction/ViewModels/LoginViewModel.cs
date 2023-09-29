using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "1";

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _password = "2";

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public void Login()
        {
            Debug.WriteLine("test");
        }


    }
}
