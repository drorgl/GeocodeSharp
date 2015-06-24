using System;
using System.Linq;
using System.Threading.Tasks;
using GeocodeSharp.Google;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeocodeSharp.Tests.Google
{
    [TestClass]
    public class GeocodeClientTest
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task TestGeocodeAddressZeroResults()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeAddress("");
            Assert.Fail();
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task TestGeocodeAddressWithNullAddress()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            await client.GeocodeAddress(null);
            Assert.Fail();
        }

        [TestMethod]
        public async Task TestGeocodeAddressWithPartialMatch()
        {
            const string address = "21 Henr St, Bristol, UK";
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeAddress(address);
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual(true, result.Results.All(r => r.PartialMatch));
            Assert.AreEqual(true, result.Results.Length > 0);
        }

        [TestMethod]
        public async Task TestGeocodeAddressWithPartialMatchViaGeocodeRequest()
        {
            const string address = "21 Henr St, Bristol, UK";
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(new GeocodeRequest { address = address });
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual(true, result.Results.All(r => r.PartialMatch));
            Assert.AreEqual(true, result.Results.Length > 0);
        }

        [TestMethod]
        public async Task TestTestGeocodeAddressWithExactMatch()
        {
            const string address = "21 Henrietta St, Bristol, UK";
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var response = await client.GeocodeAddress(address);
            Assert.AreEqual(GeocodeStatus.Ok, response.Status);
            Assert.AreEqual(false, response.Results.All(r => r.PartialMatch));
            Assert.AreEqual(true, response.Results.Length == 1);
            var result = response.Results[0];
            Assert.AreEqual("21 Henrietta Street, Bristol, City of Bristol BS5 6HU, UK", result.FormattedAddress);
            Assert.AreEqual(51, (int)result.Geometry.Location.Latitude);
            Assert.AreEqual(-2, (int)result.Geometry.Location.Longitude);
            Assert.IsTrue(result.Types.Contains("street_address"));
        }

        [TestMethod]
        public async Task TestGeocodeAddressWithRegion()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeAddress("London", region: "ca");
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual("London, ON, Canada", result.Results.First().FormattedAddress);

            result = await client.GeocodeAddress("London", region: "uk");
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual("London, UK", result.Results.First().FormattedAddress);
        }

        [TestMethod]
        public async Task TestGeocodeAddressWithRegionViaGeocodeRequest()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(new GeocodeRequest { address = "London", region = "ca" });
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual("London, ON, Canada", result.Results.First().FormattedAddress);

            result = await client.GeocodeRequest(new GeocodeRequest { address = "London", region = "uk" });
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual("London, UK", result.Results.First().FormattedAddress);
        }

        [TestMethod]
        public async Task TestGeocodeAddressWithRegionBoundsComponentsAndLanguageViaGeocodeRequest()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(new GeocodeRequest { 
                address = "London", 
                region = "ca",
                bounds = new GeoViewport{ 
                    Northeast = new GeoCoordinate{Latitude = -51.5073509m, Longitude = -0.1277583m }, 
                    Southwest = new GeoCoordinate{Latitude = 51.5073509m, Longitude = 0.1277583m } },
                components = GeocodeComponent.locality, 
                language = "en-GB"
            });
            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.AreEqual("London, ON, Canada", result.Results.First().FormattedAddress);
        }

        [TestMethod]
        public async Task TestReverseGeocodeViaGeocodeReverseRequest()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(new GeocodeReverseRequest { latlng = new GeoCoordinate { Latitude = 51.5073509m, Longitude = -0.1277583m } });

            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.IsTrue(result.Results.Any(i => i.FormattedAddress == "3 Whitehall, London SW1A 2DD, UK"));
        }

        [TestMethod]
        public async Task TestReverseGeocode()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeReverse( new GeoCoordinate { Latitude = 51.5073509m, Longitude = -0.1277583m } );

            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.IsTrue(result.Results.Any(i => i.FormattedAddress == "3 Whitehall, London SW1A 2DD, UK"));
        }

        [TestMethod]
        public async Task TestReversePlaceIdViaGeocodeReverseRequest()
        {
            if (string.IsNullOrEmpty(settings.GeocodeAPIKey))
            {
                Assert.Inconclusive("APIKey was not provided");
                return;
            }

            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(new GeocodeReverseRequest { placeid = "ChIJb9KHxsgEdkgRe82UfSxQIv0" });

            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.IsTrue(result.Results.Any(i => i.FormattedAddress == "London WC2N, UK"));

        }

        [TestMethod]
        public async Task TestReversePlaceId()
        {
            if (string.IsNullOrEmpty(settings.GeocodeAPIKey))
            {
                Assert.Inconclusive("APIKey was not provided");
                return;
            }
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeReverse("ChIJb9KHxsgEdkgRe82UfSxQIv0");

            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.IsTrue(result.Results.Any(i => i.FormattedAddress == "London WC2N, UK"));

        }


        [TestMethod]
        public async Task TestReverseGeocodeWithLanguageResultViaGeocodeReverseRequest()
        {
            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(
                new GeocodeReverseRequest { 
                    latlng = new GeoCoordinate { Latitude = 51.5073509m, Longitude = -0.1277583m },
                    language = "en-GB", 
                });

            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.IsTrue(result.Results.Any(i => i.FormattedAddress == "3 Whitehall, London SW1A 2DD, UK"));
        }

        [TestMethod]
        public async Task TestReverseGeocodeWithLocationResultTypeLanguageResultViaGeocodeReverseRequest()
        {
            if (string.IsNullOrEmpty(settings.GeocodeAPIKey))
            {
                Assert.Inconclusive("APIKey was not provided");
                return;
            }


            var client = new GeocodeClient(settings.GeocodeAPIKey);
            var result = await client.GeocodeRequest(
                new GeocodeReverseRequest
                {
                    latlng = new GeoCoordinate { Latitude = 51.5073509m, Longitude = -0.1277583m },
                    language = "en-GB",
                    location_type = LocationType.Approximate,
                    result_type = GeocodeComponent.locality
                });

            Assert.AreEqual(GeocodeStatus.Ok, result.Status);
            Assert.IsTrue(result.Results.Any(i => i.FormattedAddress == "London, UK"));
        }


    }
}
