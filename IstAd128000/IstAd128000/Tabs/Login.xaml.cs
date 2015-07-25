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
using InstAd128000.Helpers;
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
            UsernameBox.Text = Properties.Settings.Default.Username;
            PasswordBox.Password= Properties.Settings.Default.Password;
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

            ControlGetter.MainWindow.Panel.Children.Clear();
            SpinnerInstance.SetToMainWindow();

            if (error) return;

            if (await DoLoginTaskAsync())
            {
                ControlGetter.MainWindow.IsLogged = true;
                ControlGetter.MainWindow.Panel.Children.Clear();
            }
            else
            {
                var warnText = new TextBlock();
                warnText.Style = (Style)Application.Current.MainWindow.FindResource("OnStartDisclaimer");
                warnText.Text = "Credentials provided are incorrect or do not exist. Please, ckeck it and try again.";
                warnText.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                warnText.VerticalAlignment = VerticalAlignment.Top;

                ControlGetter.MainWindow.IsLogged = false;
                ControlGetter.MainWindow.Panel.Children.Clear();
                ControlGetter.MainWindow.Panel.Children.Add(warnText);
                ControlGetter.MainWindow.Panel.Children.Add(this);
            }
        }

        private async Task<bool> DoLoginTaskAsync()
        {
            ControlGetter.MainWindow.User = new InstaUser(Properties.Settings.Default.ClientKey,
                Properties.Settings.Default.ClientId, Driver.Instance, UsernameBox.Text,
                PasswordBox.Password);
            var task = new Task<bool>(ControlGetter.MainWindow.User.Authorize);
            task.Start();
            return await task;
        }
    }
}
