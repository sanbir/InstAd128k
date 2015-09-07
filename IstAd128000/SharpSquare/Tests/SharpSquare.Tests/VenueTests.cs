using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace SharpSquare.Tests
{
    public class VenueTests : TestBase
    {
        [Fact]
        public void SearchVenues()
        {
            var venues = client.SearchVenues(new Dictionary<string, string>
            {
                {"ll", "54.8693482,83.0785167"},
                {"radius", $"{"2000"}"},
                {"query", "суши"}
            });

            venues.Count.ShouldBeGreaterThan(0);
        }
    }
}
