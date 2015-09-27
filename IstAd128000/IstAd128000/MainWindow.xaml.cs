using System;
using System.Windows;
using System.Windows.Input;
using Instad128000.Core.Helpers.Selenium;
using InstAd128000.Properties;
using Application = System.Windows.Application;
using InstAd128000.ViewModels;

namespace InstAd128000
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = ViewModel;
            InitializeComponent();
            Width = Convert.ToDouble(Settings.Default.DefaultWidth);
            Height = Convert.ToDouble(Settings.Default.DefaultHeight);
        }

        public MainWindowViewModel ViewModel { get; set; }

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
    }
}
