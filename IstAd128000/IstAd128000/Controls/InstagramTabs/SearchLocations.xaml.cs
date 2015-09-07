using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using Instad128000.Core.Common.Logger;
using Instad128000.Core.Helpers.SearchLocations;
using InstAd128000.Properties;
using InstAd128000.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for SearchLocations.xaml
    /// </summary>
    public partial class SearchLocations
    {
        private ILogger _logger;

        private FoursquareHelper _foursquareHelper;
        public SearchLocationsViewModel ViewModel { get; set; }

        public SearchLocations()
        {
            InitializeComponent();
            MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider(Settings.Default.BingCredentialsProvider);
            //////////////////////////////////

            ViewModel = new SearchLocationsViewModel {Latitude = 54.8693482, Longitude = 83.0785167, Query = "школа", Radius = 3000, Venues = new List<Venue>()};
            DataContext = ViewModel;

            _foursquareHelper = new FoursquareHelper(Settings.Default.FourSquareClientId,
                Settings.Default.FourSquareClientSecret);
        }

        private async void SearchLocationsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Venues =
                    await
                        _foursquareHelper
                            .GetVenues(ViewModel.Latitude, ViewModel.Longitude, ViewModel.Radius, ViewModel.Query);

                AddPushpinsToMap();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private void AddPushpinsToMap()
        {
            foreach (var venue in ViewModel.Venues)
            {
                MyMap.Children.Add(new Pushpin { Location = new Location(venue.location.lat, venue.location.lng) });
            }
        }
    }
}
