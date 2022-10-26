using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpProject;
using RestSharpProject.Helpers;
using RestSharpProject.Tests;
using RestSharpProject.Tests.TestData;
using System.Net;
using System.Threading.Tasks;

namespace RestSharp
{
    [TestClass]
    public class RestSharpTests : APIBaseTest
    {
        [TestInitialize]
        public async Task TestInitialize()
        {
            #region create data
            var restResponse = await BookingHelper.AddNewBooking(RestClient);
            BookingDetails = restResponse.Data;
            #endregion

            #region assertion
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            #endregion
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            #region get data
            var getBookingResponse = await BookingHelper.GetBookingById(RestClient, BookingDetails.BookingId);
            #endregion

            #region assertion
            var expectedBookingDetails = BookingData.bookingDetails();
            Assert.AreEqual(expectedBookingDetails.Firstname, getBookingResponse.Data.Firstname, "First name did not match.");
            Assert.AreEqual(expectedBookingDetails.Lastname, getBookingResponse.Data.Lastname, "Last name did not match.");
            Assert.AreEqual(expectedBookingDetails.Totalprice, getBookingResponse.Data.Totalprice, "Total price did not match.");
            Assert.AreEqual(expectedBookingDetails.Depositpaid, getBookingResponse.Data.Depositpaid, "Deposit paid did not match.");
            Assert.AreEqual(expectedBookingDetails.Bookingdates.Checkin, getBookingResponse.Data.Bookingdates.Checkin, "Checkin dates did not match.");
            Assert.AreEqual(expectedBookingDetails.Bookingdates.Checkout, getBookingResponse.Data.Bookingdates.Checkout, "Checkout dates did not match.");
            Assert.AreEqual(expectedBookingDetails.Additionalneeds, getBookingResponse.Data.Additionalneeds, "Additional needs did not match.");
            #endregion

            #region clean data
            await BookingHelper.DeleteBookingById(RestClient, BookingDetails.BookingId);
            #endregion
        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            #region get data
            var getBookingResponse = await BookingHelper.GetBookingById(RestClient, BookingDetails.BookingId);
            #endregion

            #region update data
            var updateBookingDetails = new BookingDetailsModel()
            {
                Firstname = "Taehyung.updated",
                Lastname = "Kim.updated",
                Totalprice = getBookingResponse.Data.Totalprice,
                Depositpaid = getBookingResponse.Data.Depositpaid,
                Bookingdates = getBookingResponse.Data.Bookingdates,
                Additionalneeds = getBookingResponse.Data.Additionalneeds
            };
            var updateBooking = await BookingHelper.UpdateBookingById(RestClient, updateBookingDetails, BookingDetails.BookingId);

            Assert.AreEqual(updateBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get updated data
            var getUpdatedBookingResponse = await BookingHelper.GetBookingById(RestClient, BookingDetails.BookingId);
            #endregion

            #region assertion
            Assert.AreEqual(updateBookingDetails.Firstname, getUpdatedBookingResponse.Data.Firstname, "First name did not match.");
            Assert.AreEqual(updateBookingDetails.Lastname, getUpdatedBookingResponse.Data.Lastname, "Last name did not match.");
            Assert.AreEqual(updateBookingDetails.Totalprice, getUpdatedBookingResponse.Data.Totalprice, "Total price did not match.");
            Assert.AreEqual(updateBookingDetails.Depositpaid, getUpdatedBookingResponse.Data.Depositpaid, "Deposit paid did not match.");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkin, getUpdatedBookingResponse.Data.Bookingdates.Checkin, "Checkin dates did not match.");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkout, getUpdatedBookingResponse.Data.Bookingdates.Checkout, "Checkout dates did not match.");
            Assert.AreEqual(updateBookingDetails.Additionalneeds, getUpdatedBookingResponse.Data.Additionalneeds, "Additional needs did not match.");
            #endregion

            #region clean data
            await BookingHelper.DeleteBookingById(RestClient, BookingDetails.BookingId);
            #endregion
        }

        [TestMethod]
        public async Task DeleteBooking()
        {
            #region delete data
            var deleteBooking = await BookingHelper.DeleteBookingById(RestClient, BookingDetails.BookingId);
            #endregion

            #region assertion
            Assert.AreEqual(deleteBooking.StatusCode, HttpStatusCode.Created);
            #endregion
        }

        [TestMethod]
        public async Task ValidateInvalidBookingId()
        {
            #region get created data
            var getCreatedBooking = await BookingHelper.GetBookingById(RestClient, 10080001);
            #endregion

            #region assertion
            Assert.AreEqual(getCreatedBooking.StatusCode, HttpStatusCode.NotFound);
            #endregion

            #region clean data
            await BookingHelper.DeleteBookingById(RestClient, BookingDetails.BookingId);
            #endregion
        }
    }
}
