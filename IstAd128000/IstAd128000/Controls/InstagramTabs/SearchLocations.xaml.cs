using System;
using System.Collections.Generic;
using System.Windows.Media;
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
            //////////////////////////////////

            ViewModel = new SearchLocationsViewModel {Latitude = 54.8693482, Longitude = 83.0785167, Query = "Fuck", Radius = 42};
            DataContext = ViewModel;
        }
    }
}
