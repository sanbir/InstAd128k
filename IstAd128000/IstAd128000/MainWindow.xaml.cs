using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Instad128000.Core;
using Logic.Core;
using InstAd128000.Tabs;

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
            SetIsEnabledOfOptionsTo(false);

        }

        public void SetIsEnabledOfOptionsTo(bool var)
        {
            foreach (var button in OptionsPanel.Children.OfType<Button>())
            {
                button.IsEnabled = var;
            }
        }

        private void AnyButton_OnClick(object sender, RoutedEventArgs e)
        {
            UserControl tab;

            var button = sender as Button;
            if (button == null) return;
            var tag = Convert.ToString(button.Tag);

            try
            {
                tab =
                    (UserControl)
                        Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, "InstAd128000.Tabs." + tag)
                            .Unwrap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("This functionality is under development");
                return;
            }

            tab.Width = double.NaN;
            tab.Height = double.NaN;
            Panel.Children.Clear();
            Panel.Children.Add(tab);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Driver.Close();
            Application.Current.Shutdown();
        }
    }
}
