using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using InstAd128000.Annotations;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace InstAd128000.ViewModels
{
    public class SearchLocationsViewModel : CommonViewModel, INotifyPropertyChanged
    {
        private double _latitude;
        private double _longitude;
        private int _radius;
        private string _query;
        private LocationCollection _locations;
        private LocationCollection _circleCenter;
        private List<Venue> _venues;
        private List<Venue> _chosenVenues;

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                DrawCircle();
                OnPropertyChanged(nameof(Latitude));
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                DrawCircle();
                OnPropertyChanged(nameof(Longitude));
            }
        }

        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                DrawCircle();
                OnPropertyChanged(nameof(Radius));
            }
        }

        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public LocationCollection Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }

        public LocationCollection CircleCenter
        {
            get { return _circleCenter; }
            set
            {
                _circleCenter = value;
                OnPropertyChanged(nameof(CircleCenter));
            }
        }

        public List<Venue> Venues
        {
            get { return _venues; }
            set
            {
                _venues = value;
                OnPropertyChanged(nameof(Venues));
            }
        }

        public List<Venue> ChosenVenues
        {
            get { return _venues; }
            set
            {
                _chosenVenues = value;
                OnPropertyChanged(nameof(ChosenVenues));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Calculates the end-point from a given source at a given range (meters) and bearing (degrees).
        /// This methods uses simple geometry equations to calculate the end-point.
        /// </summary>
        /// <param name="source">Point of origin</param>
        /// <param name="range">Range in meters</param>
        /// <param name="bearing">Bearing in degrees</param>
        /// <returns>End-point from the source given the desired range and bearing.</returns>
        private static Location CalculateDerivedPosition(Location source, double range, double bearing)
        {
            const double DEGREES_TO_RADIANS = Math.PI / 180D;
            const double EARTH_RADIUS_M = 6371000D;

            double latA = source.Latitude * DEGREES_TO_RADIANS;
            double lonA = source.Longitude * DEGREES_TO_RADIANS;
            double angularDistance = range / EARTH_RADIUS_M;
            double trueCourse = bearing * DEGREES_TO_RADIANS;

            double lat = Math.Asin(
                Math.Sin(latA) * Math.Cos(angularDistance) +
                Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            double dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
                Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            double lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            return new Location(
                lat / DEGREES_TO_RADIANS,
                lon / DEGREES_TO_RADIANS);
        }

        private static IEnumerable<Location> GetCircle(Location center, double radius)
        {
            for (int i = 0; i < 360; i++)
            {
                yield return CalculateDerivedPosition(center, radius, i);
            }
        }

        private static IEnumerable<Location> GetCircle(double latitude, double longitude, double radius)
        {
            var center = new Location(latitude, longitude);
            return GetCircle(center, radius);
        }

        private void DrawCircle()
        {
            DrawCircleCenter();

            var locations = GetCircle(_latitude, _longitude, _radius);
            var locationCollection = new LocationCollection();
            foreach (var location in locations)
            {
                locationCollection.Add(location);
            }
            Locations = locationCollection;
        }

        private void DrawCircleCenter()
        {
            const double centerRadius = 5;
            double decreasedRadius = _radius / centerRadius;
            decreasedRadius = decreasedRadius > centerRadius ? centerRadius : decreasedRadius;

            var locations = GetCircle(_latitude, _longitude, decreasedRadius);
            var locationCollection = new LocationCollection();
            foreach (var location in locations)
            {
                locationCollection.Add(location);
            }
            CircleCenter = locationCollection;
        }
    }
}
