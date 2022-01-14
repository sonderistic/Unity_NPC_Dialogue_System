namespace Sonderistic.Data.Geocode
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public static class GeocodeDataService
    {
        #region Constants
        private const string GEOCODE_URI = "https://geocode.xyz/";
        #endregion

        #region Methods
        public static async Task<LocationInformation> GetLocationInformation(string location)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { "locate", location },
                { "geoit", "JSON" }
            };

            LocationInformation locationInformation = await DataService.SendPostRequest<LocationInformation>(GEOCODE_URI, payload);

            // Quick and dirty way of determining availability of result
            // TODO: Know, parse, and compare geocode response codes to see type of result
            if (string.IsNullOrEmpty(locationInformation?.latitude) == true ||
                string.IsNullOrEmpty(locationInformation?.longitude) == true)
            {
                locationInformation = null;
            }

            return locationInformation;
        }
        #endregion
    }
}