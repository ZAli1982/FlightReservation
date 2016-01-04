using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Entites
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool FlightStatus { get; set; }
        public int Seats { get; set; }
        public int SoldSeats { get; set; }
        public string Aircraft { get; set; }
    }
}
