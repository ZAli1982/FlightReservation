using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Entites
{
    public class AirportFlight
    {
        public int FlightId { get; set; }
        public int ArriveAirportId { get; set; }
        public int DepartureAirportId { get; set; }
    }
}
