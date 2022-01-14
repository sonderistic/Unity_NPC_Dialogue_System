namespace Sonderistic.Data.Weather
{
    using Newtonsoft.Json.Linq;
    using Sonderistic.Data.Geocode;
    using System.Threading.Tasks;

    public static class LocationWeatherDataService
    {
        #region Constants
        private const string WEATHER_SERVICE_URI = "https://api.weather.gov/";
        #endregion

        #region Methods
        public static async Task<WeatherData> GetCurrentWeatherData(LocationInformation locationInformation)
        {
            string uri = $"{WEATHER_SERVICE_URI}/points/{locationInformation.latitude},{locationInformation.longitude}/";
            string result = await DataService.SendGetRequest(uri);
            WeatherData weatherData = null;

            if (string.IsNullOrEmpty(result) == false)
            {
                uri = JToken.Parse(result)?["properties"]?["forecast"]?.ToString();
                if (string.IsNullOrEmpty(uri) == false)
                {
                    result = await DataService.SendGetRequest(uri);
                    if (string.IsNullOrEmpty(result) == false)
                    {
                        // first element in 'periods' is the latest forecast
                        weatherData = JToken.Parse(result)?["properties"]?["periods"]?.First?.ToObject<WeatherData>();
                    }
                }
            }
            
            return weatherData;
        }
        #endregion
    }
}