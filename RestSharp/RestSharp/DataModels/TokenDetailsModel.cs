using Newtonsoft.Json;

namespace RestSharpProject.DataModels
{
    public class TokenDetailsModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
