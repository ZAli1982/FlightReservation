using Monarch.FlightReservation.Helpers;
using Monarch.FlightReservation.Service.Interface;
using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Service
{
    public class FlightReservationService : IFlightReservationService
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<AirportFlight> _airportFlightRepository;
        private readonly IRepository<TicketOffice> _ticketOfficeRepository;
     
        public FlightReservationService(IRepository<Airport> airportRepository,
                                       IRepository<Flight> flightRepository,
                                       IRepository<AirportFlight> airportFlightRepository,
                                       IRepository<TicketOffice> ticketOfficeRepository)
        {
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
            _airportFlightRepository = airportFlightRepository;
            _ticketOfficeRepository = ticketOfficeRepository;
        }

        public List<FlightDetail> GetFlightDetail(int flightId = 0)
        {
            var flightDetails = from flRep in _flightRepository.Get()
                                join afRep in _airportFlightRepository.Get() on flRep.Id equals afRep.FlightId
                                join aRep in _airportRepository.Get() on afRep.ArriveAirportId equals aRep.Id
                                join dRep in _airportRepository.Get() on afRep.DepartureAirportId equals dRep.Id
                                select new FlightDetail
                                {
                                    FlightId = flRep.Id,
                                    ArrivalAirport = aRep.Name,
                                    DepartureAirport = dRep.Name,
                                    ArrivalTime = flRep.ArrivalTime,
                                    DepartureTime = flRep.DepartureTime,
                                    FlightStatus = flRep.FlightStatus == false ? "Cancelled" : "Scheduled",
                                    SoldSeat = flRep.SoldSeats,
                                    Seats = flRep.Seats
                                };
            if (flightId == 0)
            {
                return flightDetails.ToList();
            }
            else
            {
                return flightDetails.Where(x => x.FlightId == flightId).ToList();
            }

        }

        public List<FlightDetail> GetOrderedFlightDetail(string orderby)
        {
            var orderColumn = typeof(FlightDetail).GetProperty(orderby);
            return GetFlightDetail().OrderBy(x => orderColumn.GetValue(x,null)).ToList();
        }

        public List<FlightDetail> GetDepartureFlight(string ISOcode)
        {
            string airport =  _airportRepository.Get().FirstOrDefault(x => x.IsoCode == ISOcode).Name;
            return GetFlightDetail().Where(x => x.DepartureAirport == airport).ToList();
        }

        public void CancelFlight(int Id)
        {
            Flight flight = _flightRepository.Get().FirstOrDefault(x => x.Id == Id);
            flight.FlightStatus = false;
            _flightRepository.Update(flight);
        }

        public List<FlightDetail> GetArrivalFlight(string ISOcode)
        {
            string airport = _airportRepository.Get().FirstOrDefault(x => x.IsoCode == ISOcode).Name;
            return GetFlightDetail().Where(x => x.ArrivalAirport == airport).ToList();
        }

        public List<FlightDetail> GetFutureFlights(bool includeFullyBookedFlight)
        {
            if (includeFullyBookedFlight)
            {
                return GetFlightDetail().Where(x => x.DepartureTime > DateTime.Now && x.FlightStatus != "Cancelled").ToList();
            }
            else
            {
                return GetFlightDetail().Where(x => x.DepartureTime > DateTime.Now && x.FlightStatus != "Cancelled" && x.SoldSeat < x.Seats).ToList();
            }
        }

        public void AddFlight(FlightDetail flightDetail)
        {
            Flight flight = new Flight
            {
                ArrivalTime = flightDetail.ArrivalTime,
                DepartureTime = flightDetail.DepartureTime,
                Id = _flightRepository.Get().Count() + 1
            };

            AirportFlight airportFlight = new AirportFlight()
            {
                ArriveAirportId = _airportRepository.Get().FirstOrDefault(x => x.Name == flightDetail.ArrivalAirport).Id,
                DepartureAirportId = _airportRepository.Get().FirstOrDefault(x => x.Name == flightDetail.DepartureAirport).Id,
                FlightId = _flightRepository.Get().Count()
            };

            _flightRepository.Add(flight);
            _airportFlightRepository.Add(airportFlight);
        }

        public FlightDetail PurchaseTicket(int flightId)
        {
            var flight = _flightRepository.Get().FirstOrDefault(x => x.Id == flightId);
            int soldSeat = flight.SoldSeats; 
            if (flight.SoldSeats <= flight.Seats )
            {
                flight.SoldSeats = soldSeat + 1; 
            }

            var ticketoffice = _ticketOfficeRepository.Get().FirstOrDefault(x => x.FlightID == flightId);
            PromotionSales promotionsale = new PromotionSales(ticketoffice);
            var ticket = GetFlightDetail(flightId).FirstOrDefault();
            ticket.Cost = promotionsale.CheckDiscounts();
            
            return ticket;
        }

    }
}
