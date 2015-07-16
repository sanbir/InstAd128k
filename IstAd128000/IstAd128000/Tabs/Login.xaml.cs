﻿using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
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
        }

        private bool _success = false ;
        private InstaUser _user;

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).LoginButton.IsEnabled = false;
            ((MainWindow)Application.Current.MainWindow).Panel.Children.Add(new Spinner());

            var error = false;

            if (string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                UsernameBox.Background = new SolidColorBrush(Color.FromArgb(0x5a, 0xFF, 0, 0));
                UsernameBox.Text = "Please, provide username!";
                error = true;
            }
            if (string.IsNullOrWhiteSpace(PasswordBox.Text))
            {
                PasswordBox.Background = new SolidColorBrush(Color.FromArgb(0x5a, 0xFF, 0, 0));
                PasswordBox.Text = "Please, provide password!";
                error = true;
            }

            if (error) return;

            var task = DoLoginTaskAsync();
            if (await task)
            {
                ((MainWindow)Application.Current.MainWindow).Panel.Children.Clear();
                ((MainWindow)Application.Current.MainWindow).SetIsEnabledOfOptionsTo(true);
            }
            else
            {
                var warnText = new TextBlock();
                warnText.Style = (Style)Application.Current.MainWindow.FindResource("OnStartDisclaimer");
                warnText.Text = "Credentials provided are incorrect or do not exist. Please, ckeck it and try again.";
                warnText.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
                warnText.VerticalAlignment = VerticalAlignment.Top;

                ((MainWindow)Application.Current.MainWindow).Panel.Children.Clear();
                ((MainWindow)Application.Current.MainWindow).Panel.Children.Add(warnText);
                ((MainWindow)Application.Current.MainWindow).Panel.Children.Add(this);
                ((MainWindow)Application.Current.MainWindow).LoginButton.IsEnabled = true;
            }
        }

        private async Task<bool> DoLoginTaskAsync()
        {
            _user = new InstaUser(System.Configuration.ConfigurationManager.AppSettings["clientKey"],
                System.Configuration.ConfigurationManager.AppSettings["clientId"], Driver.Instance, UsernameBox.Text,
                PasswordBox.Text);

            return await new Task<bool>(_user.Authorize);
        }
    }
}
