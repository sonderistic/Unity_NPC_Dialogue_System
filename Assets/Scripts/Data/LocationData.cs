namespace Sonderistic.Data.Geocode
{
    using Newtonsoft.Json;

    public class LocationInformation
    {
        #region Variables
        [JsonProperty("latt")]
        private string _latitude;
        [JsonProperty("longt")]
        private string _longitude;
        #endregion

        #region Properties
        public string latitude
        {
            get
            {
                return _latitude;
            }
        }

        public string longitude
        {
            get
            {
                return _longitude;
            }
        }
        #endregion
    }
}