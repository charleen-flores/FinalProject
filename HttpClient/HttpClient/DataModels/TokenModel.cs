using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientLibrary.DataModels
{
    class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
