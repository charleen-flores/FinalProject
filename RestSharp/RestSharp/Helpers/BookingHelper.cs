using RestSharpProject.Tests.TestData;
using RestSharp;
using System.Threading.Tasks;
using RestSharpProject.DataModels;
using RestSharpProject.Resources;

namespace RestSharpProject.Helpers
{
    public class BookingHelper
    {
        public static async Task<RestResponse<BookingModel>> AddNewBooking(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(Endpoint.BaseBookingMethod).AddJsonBody(BookingData.bookingDetails());

            return await restClient.ExecutePostAsync<BookingModel>(postRequest);

        }

        public static async Task<RestResponse<BookingDetailsModel>> GetBookingById(RestClient restClient, int bookingId)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var getRequest = new RestRequest(Endpoint.MethodByBookingById(bookingId));

            return await restClient.ExecuteGetAsync<BookingDetailsModel>(getRequest);
        }

        public static async Task<RestResponse> DeleteBookingById(RestClient restClient, int bookingId)
        {
            var token = await GetAuthToken(restClient);
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            var getRequest = new RestRequest(Endpoint.MethodByBookingById(bookingId));

            return await restClient.DeleteAsync(getRequest);
        }

        public static async Task<RestResponse<BookingDetailsModel>> UpdateBookingById(RestClient restClient, BookingDetailsModel booking, int bookingId)
        {
            var token = await GetAuthToken(restClient);
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            var putRequest = new RestRequest(Endpoint.MethodByBookingById(bookingId)).AddJsonBody(booking);

            return await restClient.ExecutePutAsync<BookingDetailsModel>(putRequest);
        }

        private static async Task<string> GetAuthToken(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(Endpoint.GenerateToken).AddJsonBody(AuthData.credentials());

            var generateToken = await restClient.ExecutePostAsync<TokenModel>(postRequest);

            return generateToken.Data.Token;
        }
    }
}
