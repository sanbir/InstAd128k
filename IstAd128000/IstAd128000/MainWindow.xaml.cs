using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        private void AnyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var tag = Convert.ToString((sender as Button).Tag);
            var tab = (UserControl)Activator.CreateInstance(Type.GetType("InstAd128000.Tabs." + tag + ", InstAd128000"));

            Panel.Children.Add(tab);
        }
    }
}
