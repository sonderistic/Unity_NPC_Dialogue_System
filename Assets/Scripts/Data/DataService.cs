namespace Sonderistic.Data
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public static class DataService
    {
        #region Variables
        private static readonly HttpClient client = new HttpClient();
        #endregion

        #region Methods
        public static async Task<string> SendPostRequest(string uri, Dictionary<string, string> payload, string auth = null)
        {
            if (string.IsNullOrEmpty(auth) == false)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            }

            FormUrlEncodedContent content = new FormUrlEncodedContent(payload);
            HttpResponseMessage response = await client.PostAsync(uri, content);
            string result = null;

            if (response.IsSuccessStatusCode == true)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        public static async Task<T> SendPostRequest<T>(string uri, Dictionary<string, string> payload, string auth = null)
        {
            T objectResult = default;
            
            string responseResult = await SendPostRequest(uri, payload, auth);
            if (string.IsNullOrEmpty(responseResult) == false)
            {
                objectResult = JsonConvert.DeserializeObject<T>(responseResult);
            }

            return objectResult;
        }

        public static async Task<string> SendGetRequest(string uri, string auth = null)
        {
            if (string.IsNullOrEmpty(auth) == false)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            }

            HttpResponseMessage response = await client.GetAsync(uri);
            string result = null; 

            if (response.IsSuccessStatusCode == true)
            {
                result = await response.Content.ReadAsStringAsync(); 
            }

            return result;
        }

        public static async Task<T> SendGetRequest<T>(string uri, string auth = null)
        {
            T objectResult = default;

            string responseResult = await SendGetRequest(uri, auth);
            if (string.IsNullOrEmpty(responseResult) == false)
            {
                objectResult = JsonConvert.DeserializeObject<T>(responseResult);
            }

            return objectResult;
        }
        #endregion

        #region Constructors
        static DataService()
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Sonderistic", "1.0"));
        }
        #endregion
    }
}