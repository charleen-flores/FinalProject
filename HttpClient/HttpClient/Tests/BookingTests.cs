using HttpClientLibrary.DataModels;
using HttpClientLibrary.Helpers;
using HttpClientLibrary.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace HttpClientLibrary
{
    [TestClass]
    public class BookingTests
    {
        private BookingHelper bookingHelper;

        [TestMethod]
        public async Task CreateBooking()
        { 
            bookingHelper = new BookingHelper();

            #region create data
            var addBooking = await bookingHelper.AddNewBooking();
            var getResponse = JsonConvert.DeserializeObject<BookingModel>(addBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(addBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get data
            var getBooking = await bookingHelper.GetBookingById(getResponse.BookingId);
            var getBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region cleandata
            await bookingHelper.DeleteBookingById(getResponse.BookingId);
            #endregion

            #region assertion
            var expectedBookingDetails = BookingData.bookingDetails();
            Assert.AreEqual(expectedBookingDetails.Firstname, getBookingResponse.Firstname, "First name did not match.");
            Assert.AreEqual(expectedBookingDetails.Lastname, getBookingResponse.Lastname, "Last name did not match.");
            Assert.AreEqual(expectedBookingDetails.Totalprice, getBookingResponse.Totalprice, "Total price did not match.");
            Assert.AreEqual(expectedBookingDetails.Depositpaid, getBookingResponse.Depositpaid, "Deposit paid did not match.");
            Assert.AreEqual(expectedBookingDetails.Bookingdates.Checkin, getBookingResponse.Bookingdates.Checkin, "Checkin dates did not match.");
            Assert.AreEqual(expectedBookingDetails.Bookingdates.Checkout, getBookingResponse.Bookingdates.Checkout, "Checkout dates did not match.");
            Assert.AreEqual(expectedBookingDetails.Additionalneeds, getBookingResponse.Additionalneeds, "Additional needs did not match.");
            #endregion
        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            bookingHelper = new BookingHelper();

            #region create data
            var addBooking = await bookingHelper.AddNewBooking();
            var getResponse = JsonConvert.DeserializeObject<BookingModel>(addBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(addBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get data
            var getBooking = await bookingHelper.GetBookingById(getResponse.BookingId);
            var getBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region update data
            var updateBookingDetails = new BookingDetailsModel()
            {
                Firstname = "Taehyung.updated",
                Lastname = "Kim.updated",
                Totalprice = getBookingResponse.Totalprice,
                Depositpaid = getBookingResponse.Depositpaid,
                Bookingdates = getBookingResponse.Bookingdates,
                Additionalneeds = getBookingResponse.Additionalneeds
            };
            var updateBooking = await bookingHelper.UpdateBookingById(updateBookingDetails, getResponse.BookingId);
            var getUpdatedResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(updateBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(updateBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get updated data
            var getUpdatedBooking = await bookingHelper.GetBookingById(getResponse.BookingId);
            var getUpdatedBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getUpdatedBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region assertion
            Assert.AreEqual(updateBookingDetails.Firstname, getUpdatedBookingResponse.Firstname, "First name did not match.");
            Assert.AreEqual(updateBookingDetails.Lastname, getUpdatedBookingResponse.Lastname, "Last name did not match.");
            Assert.AreEqual(updateBookingDetails.Totalprice, getUpdatedBookingResponse.Totalprice, "Total price did not match.");
            Assert.AreEqual(updateBookingDetails.Depositpaid, getUpdatedBookingResponse.Depositpaid, "Deposit paid did not match.");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkin, getUpdatedBookingResponse.Bookingdates.Checkin, "Checkin dates did not match.");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkout, getUpdatedBookingResponse.Bookingdates.Checkout, "Checkout dates did not match.");
            Assert.AreEqual(updateBookingDetails.Additionalneeds, getUpdatedBookingResponse.Additionalneeds, "Additional needs did not match.");
            #endregion

            #region clean data
            await bookingHelper.DeleteBookingById(getResponse.BookingId);
            #endregion

        }

        [TestMethod]
        public async Task DeleteBooking()
        {
            bookingHelper = new BookingHelper();

            #region create data
            var addBooking = await bookingHelper.AddNewBooking();
            var getResponse = JsonConvert.DeserializeObject<BookingModel>(addBooking.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(addBooking.StatusCode, HttpStatusCode.OK);
            #endregion

            #region get data
            var getBooking = await bookingHelper.GetBookingById(getResponse.BookingId);
            var getBookingResponse = JsonConvert.DeserializeObject<BookingDetailsModel>(getBooking.Content.ReadAsStringAsync().Result);
            #endregion

            #region delete data
            var deleteBooking = await bookingHelper.DeleteBookingById(getResponse.BookingId);
            #endregion

            #region assertion
            Assert.AreEqual(deleteBooking.StatusCode, HttpStatusCode.Created);
            #endregion
        }

        [TestMethod]
        public async Task ValidateInvalidBookingId()
        {
            bookingHelper = new BookingHelper();

            #region get data
            var getCreatedBooking = await bookingHelper.GetBookingById(10080001);
            #endregion

            #region assertion
            Assert.AreEqual(getCreatedBooking.StatusCode, HttpStatusCode.NotFound);
            #endregion
        }
    }
}