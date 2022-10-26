using RestSharpProject.DataModels;

namespace RestSharpProject.Tests.TestData
{
    public class AuthData
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
