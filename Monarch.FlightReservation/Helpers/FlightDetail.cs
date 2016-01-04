using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monarch.FlightReservation.Helpers
{
    public class FlightDetail
    {
        public int FlightId { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string FlightStatus { get; set; }
        public int Seats { get; set; }
        public int SoldSeat { get; set; }
        public decimal Cost { get; set; }
    }
}
