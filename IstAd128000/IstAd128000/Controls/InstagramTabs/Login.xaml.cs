using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Instad128000.Core.Common.Interfaces;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Helpers.Selenium;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using InstAd128000.Helpers;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, IDBInteractive
    {
        public IRequestService RequestService { get; set; }
        public IDataStringService DataStringService { get; set; }

        public Login()
        {
            InitializeComponent();
            UsernameBox.Text = Properties.Settings.Default.Username;
            PasswordBox.Password = Properties.Settings.Default.Password;
        }

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            var error = false;

            if (string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                UsernameBox.Background = new SolidColorBrush(Color.FromArgb(60, 255, 100, 0));
                UsernameBox.Text = "Please, provide username!";
                error = true;
            }
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Background = new SolidColorBrush(Color.FromArgb(60, 255, 100, 0));
                PasswordBox.Password = "Please, provide password!";
                error = true;
            }

            if (UsernameBox.Text != Properties.Settings.Default.Username ||
                PasswordBox.Password != Properties.Settings.Default.Password)
            {
                var result = MessageBox.Show("Do you want to save credentials?","",MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Properties.Settings.Default.Username = UsernameBox.Text;
                    Properties.Settings.Default.Password = PasswordBox.Password;
                    Properties.Settings.Default.Save();
                }
            }

            ControlGetter.MainWindow.InstagramTab.Panel.Children.Clear();
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = false;
            SpinnerInstance.SetToMainWindow();

            if (error) return;

            if (await DoLoginTaskAsync())
            {
                ControlGetter.MainWindow.InstagramTab.IsLogged = true;
                ControlGetter.MainWindow.InstagramTab.Panel.Children.Clear();
            }
            else
            {
                var warnText = new TextBlock();
                warnText.Style = (Style)Application.Current.MainWindow.FindResource("OnStartDisclaimer");
                warnText.Text = "Credentials provided are incorrect or do not exist. Please, ckeck it and try again.";
                warnText.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                warnText.VerticalAlignment = VerticalAlignment.Top;

                ControlGetter.MainWindow.InstagramTab.IsLogged = false;
                ControlGetter.MainWindow.InstagramTab.Panel.Children.Clear();
                ControlGetter.MainWindow.InstagramTab.Panel.Children.Add(warnText);
                ControlGetter.MainWindow.InstagramTab.Panel.Children.Add(this);
            }
            ControlGetter.MainWindow.InstagramTab.IsNoProcessPerformed = true;
        }

        private async Task<bool> DoLoginTaskAsync()
        {
            ControlGetter.MainWindow.InstagramTab.User = new InstagramUser(Properties.Settings.Default.ClientKey,
                Properties.Settings.Default.ClientId, Driver.Instance, UsernameBox.Text,
                PasswordBox.Password, ControlGetter.MainWindow.RequestService, ControlGetter.MainWindow.DataStringService);
            var task = new Task<bool>(ControlGetter.MainWindow.InstagramTab.User.Authorize);
            task.Start();
            return await task;
        }
    }
}
