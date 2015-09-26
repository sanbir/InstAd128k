using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
using Instad128000.Core.Helpers.SocialNetworksUsers;

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
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            DataContext = ViewModel;

            _foursquareHelper = new FoursquareHelper(Settings.Default.FourSquareClientId,
                Settings.Default.FourSquareClientSecret);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // refresh the map
            MyMap.SetView(MyMap.Center, MyMap.ZoomLevel * 1.0001);
            MyMap.SetView(MyMap.Center, MyMap.ZoomLevel / 1.0001);
        }

        private async void SearchLocationsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Venues.Clear();

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

        private async void MyMap_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Disables the default mouse click action.
            e.Handled = true;

            //Get the mouse click coordinates
            Point mousePosition = e.GetPosition(MyMap);

            //Convert the mouse coordinates to a locatoin on the map
            Location circleCenter = MyMap.ViewportPointToLocation(mousePosition);

            ViewModel.Latitude = circleCenter.Latitude;
            ViewModel.Longitude = circleCenter.Longitude;
        }

        private void SaveLocations_Click(object sender, RoutedEventArgs e)
        {
            UserFactory.Insta.LocationsToProcess = ViewModel.Venues;
            var result = MessageBox.Show("Локации сохранены, доступны во вкладках \"Комменты\" и \"Лайки\"");
        }
    }
}
