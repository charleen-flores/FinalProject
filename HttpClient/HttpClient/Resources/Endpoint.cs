using System;

namespace HttpClientLibrary.Resources
{
    public class Endpoint
    {
        //Base URL
        public const string baseURL = "https://restful-booker.herokuapp.com/";

        public const string UserEndpoint = "booking";

        public const string AuthEndpoint = "auth";

        public static string GetURL(string endpoint) => $"{baseURL}{endpoint}";

        public static Uri GetUri(string endpoint) => new Uri(GetURL(endpoint));
    }
}
