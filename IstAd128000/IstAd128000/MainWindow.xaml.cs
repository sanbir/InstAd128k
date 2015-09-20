using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Instad128000.Core;
using Instad128000.Core.Common.Interfaces.Services;
using Instad128000.Core.Helpers.Selenium;
using Instad128000.Core.Helpers.SocialNetworksUsers;
using InstAd128000.Properties;
using InstAd128000.Services;
using Microsoft.Practices.Unity;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace InstAd128000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Width = Convert.ToDouble(Settings.Default.DefaultWidth);
            Height = Convert.ToDouble(Settings.Default.DefaultHeight);
        }

        [Dependency]
        public IRequestService RequestService { get; set; }
        [Dependency]
        public IDataStringService DataStringService { get; set; }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState != MouseButtonState.Released)
                this.DragMove();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Driver.Close();
            Application.Current.Shutdown();
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void DefaultSize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            this.Width = Convert.ToDouble(Settings.Default.DefaultWidth);
            this.Height = Convert.ToDouble(Settings.Default.DefaultHeight);
        }

        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This functionality is under development");
        }
    }
}
