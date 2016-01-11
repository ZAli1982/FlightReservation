//using Monarch.FlightReservation.Model.Entites;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Monarch.FlightReservation.Model
//{
//    public static class TestDatabase
//    {
//        private static List<Flight> _flights;
//        private static List<AirportFlight> _airportFlights;

//        static TestDatabase()
//        {
//            _flights = new List<Flight> 
//            {
//                new Flight { Id = 1, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0), FlightStatus = true, Aircraft="A320", Seats=175, SoldSeats=175},
//                new Flight { Id = 2, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2, 11, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2, 12, 0, 0), FlightStatus = true, Aircraft="A320", Seats=175, SoldSeats=175},
//                new Flight { Id = 3, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 2, 10, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 14, 0, 0), FlightStatus = true, Aircraft="A320", Seats=175, SoldSeats=172},
//                new Flight { Id = 4, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 21, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 16, 0, 0), FlightStatus = false , Aircraft="A300", Seats=230, SoldSeats=230},
//                new Flight { Id = 5, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 14, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 18, 0, 0), FlightStatus = true , Aircraft="A320", Seats=175, SoldSeats=165},
//                new Flight { Id = 6, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 22, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 17, 0, 0), FlightStatus = true , Aircraft="A320", Seats=175, SoldSeats=175}
//            };

//            _airportFlights = new List<AirportFlight> 
//                {
//                    new AirportFlight { ArriveAirportId = 5, DepartureAirportId = 1, FlightId = 1 },
//                    new AirportFlight { ArriveAirportId = 2, DepartureAirportId = 3,  FlightId = 2 },
//                    new AirportFlight { ArriveAirportId = 3, DepartureAirportId = 1, FlightId = 3 },
//                    new AirportFlight { ArriveAirportId = 2, DepartureAirportId = 3, FlightId = 4 },
//                    new AirportFlight { ArriveAirportId = 5, DepartureAirportId = 4, FlightId = 5 },
//                    new AirportFlight { ArriveAirportId = 1, DepartureAirportId = 5, FlightId = 6 },
//                };
//        }

//        public static List<Flight> Flights 
//        { 
//            get 
//            {
//                return _flights;
//            } 
//            set
//            {
//                _flights = value;
//            }
//        }

//        public static List<TicketOffice> TicketOffice
//        {
//            get
//            {
//                return new List<TicketOffice> 
//                {
//                    new TicketOffice { Id = 1, FlightID=3, Cost=45, TicketType="ShortHaul", Return=false, Discount=0 },
//                    new TicketOffice { Id = 1, FlightID=3, Cost=75, TicketType="LongHaul", Return=false, Discount=0 },
//                    new TicketOffice { Id = 1, FlightID=5, Cost=75, TicketType="LongHaul", Return=false, Discount=20 }
//                };
//            }
//            set
//            {

//            }
//        }

//        public static List<Airport> Airports
//        {
//            get
//            {
//                return new List<Airport> 
//                {
//                    new Airport { Id = 1, Name = "Luton", IsoCode = "LTN" },
//                    new Airport { Id = 2, Name = "Gatwick", IsoCode = "LGW" },
//                    new Airport { Id = 3, Name = "Manchester", IsoCode ="MAN" },
//                    new Airport { Id = 4, Name = "Amsterdam", IsoCode ="AMS" },
//                    new Airport { Id = 5, Name = "Barcelona", IsoCode ="BCN" }
//                };
//            }
//            set
//            {

//            }
//        }

//        public static List<AirportFlight> AirportFlights
//        {
//            get
//            {
//                return _airportFlights;
//            }
//            set
//            {
//                _airportFlights = value;
//            }
//        }
//    }
//}
