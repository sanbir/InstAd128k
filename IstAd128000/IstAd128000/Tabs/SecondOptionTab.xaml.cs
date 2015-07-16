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
using InstAd128000.Controls;

namespace InstAd128000.Tabs
{
    /// <summary>
    /// Interaction logic for SecondOptionTag.xaml
    /// </summary>
    public partial class SecondOptionTab : UserControl
    {
        public SecondOptionTab()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Panel.Children.Clear();
            ((MainWindow) Application.Current.MainWindow).Panel.Children.Add(new Spinner());
        }
    }
}
