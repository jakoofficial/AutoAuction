using AutoAuction.DatabaseFiles;
using AutoAuction.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        string usernameTxt = User.Instance.UserName;
        public string UsernameTxt
        {
            get => usernameTxt;
            set => this.RaiseAndSetIfChanged(ref usernameTxt, value);
        }

        decimal balanceTxt = User.Instance.Balance;
        public decimal BalanceTxt
        {
            get => balanceTxt;
            set => this.RaiseAndSetIfChanged(ref balanceTxt, value);
        }

        string auctionsWon = User.Instance.AuctionsWon;
        public string AuctionsWon
        {
            get => auctionsWon;
            set => this.RaiseAndSetIfChanged(ref auctionsWon, value);
        }

        string yourAuctions = User.Instance.YourAuctions;
        public string YourAuctions
        {
            get => yourAuctions;
            set => this.RaiseAndSetIfChanged(ref yourAuctions, value);
        }

        decimal creditTxt;
        public decimal CreditTxt
        {
            get => creditTxt;
            set => this.RaiseAndSetIfChanged(ref creditTxt, value);
        }
        bool isCorpUser = false;
        public bool IsCorpUser
        {
            get => isCorpUser;
            set => this.RaiseAndSetIfChanged(ref isCorpUser, value);
        }

        bool changePasswordBtn = false;
        public bool ChangePasswordBtn
        {
            get => changePasswordBtn;
            set => this.RaiseAndSetIfChanged(ref changePasswordBtn, value);
        }

        string newPasswordTxt = "";
        public string NewPasswordTxt
        {
            get => newPasswordTxt;
            set => this.RaiseAndSetIfChanged(ref newPasswordTxt, value);
        }

        string newPasswordWatermark = "New Password";
        public string NewPasswordWatermark
        {
            get => newPasswordWatermark;
            set => this.RaiseAndSetIfChanged(ref newPasswordWatermark, value);
        }

        string oldPasswordTxt = "";
        public string OldPasswordTxt
        {
            get => oldPasswordTxt;
            set => this.RaiseAndSetIfChanged(ref oldPasswordTxt, value);
        }

        string oldPasswordWatermark = "Old Password";
        public string OldPasswordWatermark
        {
            get => oldPasswordWatermark;
            set => this.RaiseAndSetIfChanged(ref oldPasswordWatermark, value);
        }

        string infoPasswordChangeTxt = "";
        public string InfoPasswordChangeTxt
        {
            get => infoPasswordChangeTxt;
            set => this.RaiseAndSetIfChanged(ref infoPasswordChangeTxt, value);
        }
        Avalonia.Media.Color infoPasswordChangeColor = Avalonia.Media.Color.Parse("#ff228b22");
        public Avalonia.Media.Color InfoPasswordChangeColor
        {
            get => infoPasswordChangeColor;
            set => this.RaiseAndSetIfChanged(ref infoPasswordChangeColor, value);
        }

        public ProfileViewModel()
        {
            User.Instance.GetAuctionsWon();
            User.Instance.CountUserAuctions();
            //Trys to TypeCast user to CorpUser to check if it is one before showing Credit or not.
            try
            {
                CorporateUser corpUser = (CorporateUser)User.Instance;
                if (corpUser != null)
                {
                    creditTxt = corpUser.Credit;
                    isCorpUser = true;

                }
            }
            catch (System.InvalidCastException)
            {
                isCorpUser = false;
            }
        }

        public void ClearTextBoxValue()
        {
            NewPasswordTxt = "";
            OldPasswordTxt = "";
        }
        /// <summary>
        /// Checks for empty Textboxes then runs the Procedure and gets 0 or 1 back depending on if it succeded or not.
        /// </summary>
        public void ConfirmChangePassword()
        {
            if (!String.IsNullOrEmpty(NewPasswordTxt) && !String.IsNullOrEmpty(oldPasswordTxt))
            {
                try
                {
                    string resultString = Database.Instance.ExecScalar($"exec ChangeUserPassword {User.Instance.UserName}, {OldPasswordTxt}, {NewPasswordTxt}");
                    int result = Convert.ToInt32(resultString);
                    if (result == 1)
                    {
                        ClearTextBoxValue();
                        Database.Instance.LogInWithUser(User.Instance.UserName, NewPasswordTxt);
                        InfoPasswordChangeColor = Avalonia.Media.Color.Parse("#ff228b22");
                        InfoPasswordChangeTxt = "Password Changed";
                        ChangePasswordBtn = false;
                    }
                    else
                    {
                        InfoPasswordChangeColor = Avalonia.Media.Color.Parse("#ff8b0000");
                        InfoPasswordChangeTxt = "Password Change Failed - Old Password Wrong might be wrong";
                    }
                }
                catch (SqlException)
                {
                    ClearTextBoxValue();
                }
            }
            else if (String.IsNullOrEmpty(newPasswordTxt) && !String.IsNullOrEmpty(oldPasswordTxt))
            {
                ClearTextBoxValue();
                NewPasswordWatermark = "NewPassword Is empty!";
            }
            else if (!String.IsNullOrEmpty(newPasswordTxt) && String.IsNullOrEmpty(oldPasswordTxt))
            {
                ClearTextBoxValue();
                OldPasswordWatermark = "OldPassword Is empty!";
            }
            else
            {
                ClearTextBoxValue();
                NewPasswordWatermark = "NewPassword Is empty!";
                OldPasswordWatermark = "OldPassword Is empty!";
            }
        }
        public void changePassword()
        {
            if (ChangePasswordBtn == false)
            {
                ChangePasswordBtn = true;
            }
            else if (ChangePasswordBtn == true) { ChangePasswordBtn = false; };
        }

        public void GoBackBtn()
        {
            SetContentArea.Navigate(new HomeScreenViewModel());
        }
    }
}
