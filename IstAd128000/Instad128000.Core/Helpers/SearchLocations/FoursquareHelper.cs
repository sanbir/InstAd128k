using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using Instad128000.Core.Common.Logger;

namespace Instad128000.Core.Helpers.SearchLocations
{
    public class FoursquareHelper
    {
        private ILogger _logger;

        readonly SharpSquare _sharpSquare;

        public FoursquareHelper(string clientId, string clientSecret)
        {
            try
            {
                _sharpSquare = new SharpSquare(clientId, clientSecret);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<Venue>> GetVenues(double latitude, double longitude, double radius, string query)
        {
            var parameters = new Dictionary<string, string>
            {
                {"ll", $"{latitude:F7},{longitude:F7}"},
                {"radius", $"{radius:F0}"},
                {"query", query}
            };

            var venuesTask = new Task<List<Venue>>(() => _sharpSquare.SearchVenues(parameters));
            venuesTask.Start();

            return await venuesTask;
        }
    }
}
