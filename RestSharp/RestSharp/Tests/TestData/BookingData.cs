using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpProject.Tests.TestData
{
    public class BookingData
    {
        public static BookingDetailsModel bookingDetails()
        {
            DateTime dt = DateTime.UtcNow.Date;

            Bookingdates bookingDates = new Bookingdates();
            bookingDates.Checkin = dt;
            bookingDates.Checkout = dt.AddDays(2);

            return new BookingDetailsModel
            {
                Firstname = "Taehyung",
                Lastname = "Kim",
                Totalprice = 100,
                Depositpaid = true,
                Bookingdates = bookingDates,
                Additionalneeds = "Breakfast"
            };
        }
    }
}
