using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientLibrary.DataModels
{
    class BookingModel
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingDetailsModel Booking { get; set; }
    }
}
