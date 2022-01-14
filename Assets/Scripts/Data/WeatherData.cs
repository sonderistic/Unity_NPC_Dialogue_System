namespace Sonderistic.Data.Weather
{
    using Newtonsoft.Json;
    
    public class WeatherData
    {
        #region Properties
        [JsonProperty]
        public string detailedForecast { get; private set; }
        #endregion
    }
}