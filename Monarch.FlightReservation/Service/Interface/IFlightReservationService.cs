using Monarch.FlightReservation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Service.Interface
{
    public interface IFlightReservationService
    {
        List<FlightDetail> GetFlightDetail(int flightId);
        void AddFlight(FlightDetail flightDetail);
        List<FlightDetail> GetOrderedFlightDetail(string orderby);
        List<FlightDetail> GetDepartureFlight(string ISOcode);
        List<FlightDetail> GetArrivalFlight(string ISOcode);
        void CancelFlight(int Id);
        List<FlightDetail> GetFutureFlights(bool includeFullyBookedFlights);
        FlightDetail PurchaseTicket(int flightId);
    }
}
