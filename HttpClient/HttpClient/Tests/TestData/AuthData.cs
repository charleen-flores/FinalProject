using HttpClientLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientLibrary.Tests.TestData
{
    class AuthData
    {
        public static TokenDetailsModel credentials()
        {
            return new TokenDetailsModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
