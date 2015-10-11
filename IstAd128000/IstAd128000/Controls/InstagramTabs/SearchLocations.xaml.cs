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
using Instad128000.Core.Common.Interfaces.Services;
using System.Collections.ObjectModel;
using Instad128000.Core.Common.Models;
using Instad128000.Core.Extensions;
using InstAd128000.Helpers;
using System.Windows.Controls;
using Instad128000.Core.Common.Exceptions;

namespace InstAd128000.Controls.InstagramTabs
{
    /// <summary>
    /// Interaction logic for SearchLocations.xaml
    /// </summary>
    public partial class SearchLocations
    {
        private ILogger _logger;

        private FoursquareHelper _foursquareHelper;

        public SearchLocations(IRequestService reqSRV, IDataStringService dataStrSRV)
        {
            InitializeComponent();
            MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider(Settings.Default.BingCredentialsProvider);
            //////////////////////////////////
            
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            _foursquareHelper = new FoursquareHelper(Settings.Default.FourSquareClientId,
                Settings.Default.FourSquareClientSecret);
            ViewModel.RequestService = reqSRV;
            ViewModel.DataStringService = dataStrSRV;
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
                IsInProgress(true);
                ViewModel.Venues = new List<Venue>();

                ViewModel.Venues =
                    await
                        _foursquareHelper
                            .GetVenues(ViewModel.Latitude, ViewModel.Longitude, ViewModel.Radius, ViewModel.Query);

                AddPushpinsToMap();
            }
            catch (InstAdException IAe)
            {
                MessageBox.Show(IAe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Системная ошибка: " + ex.Message + ". Пожалуйста, попробуйте еще раз.");
                _logger.Error(ex.Message);
            }
            finally
            {
                IsInProgress(false);
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

        protected void IsInProgress(bool isInProgress)
        {
            if (isInProgress)
            {
                UiHelper.InstaBusy(true);
                SaveLocations.IsEnabled = false;
            }
            else
            {
                UiHelper.InstaBusy(false);
                SaveLocations.IsEnabled = true;
            }
        }

        private void Found_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var text = UiHelper.FindChild<TextBlock>(sender as Grid, "LocationName").DataContext as Venue;
            if (!ViewModel.ChosenVenues.Any(x => x == text))
            {
                ViewModel.ChosenVenues = ViewModel.ChosenVenues.Concat(new[] { text });
            }
        }

        private void Chosen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var text = UiHelper.FindChild<TextBlock>(sender as Grid, "LocationName").DataContext as Venue;
            if (ViewModel.ChosenVenues.Any(x => x == text))
            {
                ViewModel.ChosenVenues = ViewModel.ChosenVenues.Except(new[] { text });
            }
        }

        private void SaveLocations_Click(object sender, RoutedEventArgs e)
        {
            var deleted = new List<Venue>();
            deleted.AddRange(UserFactory.Insta.LocationsToProcess.Where(x => !ViewModel.ChosenVenues.Any(y => y == x)));

            UserFactory.Insta.LocationsToProcess = UserFactory.Insta.LocationsToProcess.Except(deleted);
            UserFactory.Insta.LocationsToProcess = UserFactory.Insta.LocationsToProcess.Concat(ViewModel.ChosenVenues.Where(x => !UserFactory.Insta.LocationsToProcess.Any(y => y == x)));

            var result = MessageBox.Show("Локации сохранены, доступны во вкладках \"Комменты\" и \"Лайки\"");
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ChosenVenues = ViewModel.Venues;
        }

        private void DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ChosenVenues = null;
        }
    }
}
