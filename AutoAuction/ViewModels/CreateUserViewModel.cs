using AutoAuction.DatabaseFiles;
using AutoAuction.Models;
using Avalonia.Styling;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class CreateUserViewModel : ViewModelBase
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

		private string _password2nd = "";
		public string Password2nd
		{
			get => _password2nd;
			set => this.RaiseAndSetIfChanged(ref _password2nd, value);
		}

		private string _zipCode = "";
		public string ZipCode
		{
			get => _zipCode;
			set => this.RaiseAndSetIfChanged(ref _zipCode, value);
		}


		private bool _isPrivate;
		public bool IsPrivate
		{
			get => _isPrivate;
			set => this.RaiseAndSetIfChanged(ref _isPrivate, value);
		}

		private bool _isCorporate ;
		public bool IsCorporate
		{
			get => _isCorporate;
			set => this.RaiseAndSetIfChanged(ref _isCorporate, value);
		}

		private string _CPRNumber = "";
		public string CPRNumber
		{
			get => _CPRNumber;
			set => this.RaiseAndSetIfChanged(ref _CPRNumber, value);
		}

		private string _CVRNumber = "";
		public string CVRNumber
		{
			get => _CVRNumber;
			set => this.RaiseAndSetIfChanged(ref _CVRNumber, value);
		}

		private string _credit = "";
		public string Credit
		{
			get => _credit;
			set => this.RaiseAndSetIfChanged(ref _credit, value);
		}

		private bool _usernameErrorMsg;
		public bool UsernameErrorMsg
		{
			get => _usernameErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _usernameErrorMsg, value);
		}


		private bool _passwordErrorMsg;
		public bool PasswordErrorMsg
		{
			get => _passwordErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _passwordErrorMsg, value);
		}


		private bool _zipCodeErrorMsg;
		public bool ZipCodeErrorMsg
        {
			get => _zipCodeErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _zipCodeErrorMsg, value);
		}

		private bool _rbErrorMsg;
		public bool RbErrorMsg
        {
			get => _rbErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _rbErrorMsg, value);
		}

		private bool _corporateErrorMsg;
		public bool CorporateErrorMsg
		{
			get => _corporateErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _corporateErrorMsg, value);
		}

		private bool _privateErrorMsg;
		public bool PrivateErrorMsg
        {
			get => _privateErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _privateErrorMsg, value);
		}


		private string _createUserSQLErrorMsg = "";
		public string CreateUserSQLErrorMsg
        {
			get => _createUserSQLErrorMsg;
			set => this.RaiseAndSetIfChanged(ref _createUserSQLErrorMsg, value);
		}

		public void CreateUser()
		{
			uint uintZipCode;

            //Checks if the username meets the requirements 
            if (Username.Length < 4)
			{
				UsernameErrorMsg = true;
				return;
			}
			else UsernameErrorMsg = false;

            //Checks if the Password meets the requirements and both password are the same
            if (Password != Password2nd || Password.Length < 10)
			{
				PasswordErrorMsg = true;
				return;
			}
			else PasswordErrorMsg = false;

            //Checks if the zip code meets the requirements
            if (!uint.TryParse(ZipCode, out uintZipCode) || ZipCode.Length != 4)
			{
                ZipCodeErrorMsg = true;
				return;
            }
			else ZipCodeErrorMsg = false;

            //Checks if the user has chosen which type of user they are
            if (!IsPrivate && !IsCorporate)
			{
				RbErrorMsg = true;
				return;
			}
			else RbErrorMsg = false;

			//Corporate User logic
			if(IsCorporate == true)
			{
				uint uintCVRNumber;
				decimal decimalCredit;
                //Checks if the CVR number meets the requirements
                if (!uint.TryParse(CVRNumber, out uintCVRNumber) || CVRNumber.Length != 8)
				{
					CorporateErrorMsg = true;
					return;
				}
				else CorporateErrorMsg= false;

                ////Checks if the credit meets the requirements
                if (!decimal.TryParse(Credit, out decimalCredit))
				{
					CorporateErrorMsg = true;
				}
				else CorporateErrorMsg = false;

				CorporateUser cpUser = new CorporateUser(Username, Password, uintZipCode, uintCVRNumber, decimalCredit, 0M);

				string? SQLError = Database.Instance.CreateUser(cpUser);

				if (SQLError != null)
				{
					CreateUserSQLErrorMsg = SQLError;
					return;
				}
				SetContentArea.Navigate(new LoginViewModel());
                //The end of coporate user logic
            }

            //Private User Logic
            if (IsPrivate == true)
			{
				uint uintCPRNumber;

                //Checks if the CPR number meets the requirements
                if (!uint.TryParse(CPRNumber, out uintCPRNumber) || CPRNumber.Length != 10)
				{
					PrivateErrorMsg = true;
					return;
				}
				else PrivateErrorMsg = false;

				PrivateUser pUser = new PrivateUser(Username, Password, uintZipCode, CPRNumber, 0M);

                string? SQLError = Database.Instance.CreateUser(pUser);

                if (SQLError != null)
                {
                    CreateUserSQLErrorMsg = SQLError;
                    return;
                }
                SetContentArea.Navigate(new LoginViewModel());

                //The end of private user logic
            }
        }
	}
}
