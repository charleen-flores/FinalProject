using HttpClientLibrary.DataModels;
using HttpClientLibrary.Resources;
using HttpClientLibrary.Tests.TestData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientLibrary.Helpers
{
    class BookingHelper
    {
        private HttpClient httpClient;
        public async Task<HttpResponseMessage> AddNewBooking()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(BookingData.bookingDetails());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(Endpoint.GetURL(Endpoint.UserEndpoint), postRequest);
        }

        public async Task<HttpResponseMessage> GetBookingById(int bookingId)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return await httpClient.GetAsync(Endpoint.GetUri(Endpoint.UserEndpoint) + "/" + bookingId);
        }

        public async Task<HttpResponseMessage> DeleteBookingById(int bookingId)
        {
            var token = await GetAuthToken();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            return await httpClient.DeleteAsync(Endpoint.GetUri(Endpoint.UserEndpoint) + "/" + bookingId);
        }

        public async Task<HttpResponseMessage> UpdateBookingById(BookingDetailsModel booking, int bookingId)
        {
            var token = await GetAuthToken();
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            var request = JsonConvert.SerializeObject(booking);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            return await httpClient.PutAsync(Endpoint.GetURL(Endpoint.UserEndpoint + "/" + bookingId), putRequest);
        }

        private async Task<string> GetAuthToken()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(AuthData.credentials());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(Endpoint.GetURL(Endpoint.AuthEndpoint), postRequest);

            var token = JsonConvert.DeserializeObject<TokenModel>(httpResponse.Content.ReadAsStringAsync().Result);

            return token.Token;
        }
    }
}
