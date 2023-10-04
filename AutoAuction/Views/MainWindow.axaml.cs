using AutoAuction.DatabaseFiles;
using AutoAuction.Models;
using Avalonia.Controls;

namespace AutoAuction.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Database.Instance.LogInWithUser("Username", "Password");
            User.SetUser(Database.Instance.GetUser("CorpUser1"));
            InitializeComponent();
            //DataContext = new MainWindowViewModel();
        }
    }
}