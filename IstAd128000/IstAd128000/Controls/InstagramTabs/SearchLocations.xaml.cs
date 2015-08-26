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
using InstAd128000.Properties;
using InstAd128000.ViewModels;
using Microsoft.Maps.MapControl.WPF;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for SearchLocations.xaml
    /// </summary>
    public partial class SearchLocations
    {
        public SearchLocationsViewModel ViewModel { get; set; }

        public SearchLocations()
        {
            InitializeComponent();
            MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider(Settings.Default.BingCredentialsProvider);

            ViewModel = new SearchLocationsViewModel {Latitude = 300, Longitude = 544, Query = "Fuck", Radius = 42};
            DataContext = ViewModel;
        }
    }
}
