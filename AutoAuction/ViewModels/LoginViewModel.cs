using AutoAuction.DatabaseFiles;
using AutoAuction.Models;
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
        private string _username = "";

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _password = "";

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private bool _errorMsg = false;

        public bool ErrorMsg
        {
            get => _errorMsg;
            set => this.RaiseAndSetIfChanged(ref _errorMsg, value);
        }


        public void Login()
        {
            bool loginSuccessful = Database.Instance.LogInWithUser(Username, Password);
            Debug.WriteLine(loginSuccessful);
            if (loginSuccessful)
            {
                User test = Database.Instance.GetUser(Username);
            }
            else
            {
                ErrorMsg = true;
            }

        }


    }
}
