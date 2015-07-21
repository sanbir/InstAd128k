using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InstAd128000.Controls;
using Instad128000.Core;
using Instad128000.Core.Common.Logger;
using Logic.Core;
using OpenQA.Selenium.PhantomJS;

namespace InstAd128000.Tabs
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            _spinner = new Spinner();
            UsernameBox.Text = Properties.Settings.Default.Username;
            PasswordBox.Password= Properties.Settings.Default.Password;
        }

        private bool _success = false ;
        private InstaUser _user;
        private Spinner _spinner;

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            var error = false;
            var mainWindow = ((MainWindow) Application.Current.MainWindow);

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

            mainWindow.Panel.Children.Clear();
            mainWindow.Panel.Children.Add(_spinner);

            if (error) return;

            if (await DoLoginTaskAsync())
            {
                mainWindow.IsLogged = true;
                mainWindow.Panel.Children.Clear();
            }
            else
            {
                var warnText = new TextBlock();
                warnText.Style = (Style)Application.Current.MainWindow.FindResource("OnStartDisclaimer");
                warnText.Text = "Credentials provided are incorrect or do not exist. Please, ckeck it and try again.";
                warnText.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                warnText.VerticalAlignment = VerticalAlignment.Top;

                mainWindow.IsLogged = false;
                mainWindow.Panel.Children.Clear();
                mainWindow.Panel.Children.Add(warnText);
                mainWindow.Panel.Children.Add(this);
            }
        }

        private async Task<bool> DoLoginTaskAsync()
        {
            _user = new InstaUser(System.Configuration.ConfigurationManager.AppSettings["clientKey"],
                System.Configuration.ConfigurationManager.AppSettings["clientId"], Driver.Instance, UsernameBox.Text,
                PasswordBox.Password);
            var task = new Task<bool>(_user.Authorize);
            task.Start();
            return await task;
        }
    }
}
