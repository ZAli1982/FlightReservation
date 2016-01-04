using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monarch.FlightReservation.Service.Interface;
using Monarch.FlightReservation.Service;
using Monarch.FlightReservation.Model.Repositories;
using Monarch.FlightReservation.Helpers;
using Monarch.FlightReservation.Model.Entites;

namespace Monarch.FlightReservation.Test
{
    [TestFixture]
    public class FlightResevationTest
    {
        IRepository<Flight> _flightRepository;
        IRepository<Airport> _airportRepository;
        IRepository<AirportFlight> _airportFlightRepository;

        [SetUp]
        public void SetUp()
        {
            _flightRepository = new FlightRepository();
            _airportRepository = new AirportRepository();
            _airportFlightRepository = new AirportFlightRepository();
        }

        [Test]
        public void Given_i_have_a_list_of_flight_i_should_output_list_of_flights()
        { 
            //Arrange
            IFlightReservationService facade = new FlightReservationService(_airportRepository, _flightRepository, _airportFlightRepository);
            
            //Act
            var result = facade.GetFlightDetail();

            //Assert
            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void Given_i_have_added_a_new_flight_i_should_see_the_new_flight_in_list_of_flights()
        {
            //Arrange
            FlightDetail flightDetail = new FlightDetail()
            {
                ArrivalAirport = "Gatwick",
                DepartureAirport = "Luton",
                ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
                DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)
            };

            IFlightReservationService facade = new FlightReservationService(_airportRepository, _flightRepository, _airportFlightRepository);

            //Act
            facade.AddFlight(flightDetail);
            var result = facade.GetFlightDetail();

            //Assert
            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void Given_i_have_list_of_flights_it_should_orderby_departuredate()
        {
            //Arrange
            IFlightReservationService facade = new FlightReservationService(_airportRepository, _flightRepository, _airportFlightRepository);

            //Act
            var result = facade.GetFlightDetail("DepartureTime");

            //Assert
            Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0), result[0].DepartureTime);
            Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0), result[1].DepartureTime);
            Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0), result[2].DepartureTime);
        }

        [Test]
        public void Given_i_want_to_search_flights_by_depature_airport_i_should_get_filter_depature_airport_flights()
        {
            //Arrange
            IFlightReservationService facade = new FlightReservationService(_airportRepository, _flightRepository, _airportFlightRepository);

            //Act
            var result = facade.GetDepartureFlight("LTN");

            //Assert
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void Given_i_want_to_search_flights_by_arrival_airport_i_should_get_filter_arrival_airport_flights()
        {
            //Arrange
            IFlightReservationService facade = new FlightReservationService(_airportRepository, _flightRepository, _airportFlightRepository);

            //Act
            var result = facade.GetArrivalFlight("LGW");

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Given_a_flight_is_Cancel_i_should_see_the_cancelled_flight()
        {
            //Arrange
            IFlightReservationService facade = new FlightReservationService(_airportRepository, _flightRepository, _airportFlightRepository);

            //Act
            facade.CancelFlight(1);
            var result = facade.GetDepartureFlight("LTN");//.Where(x => x.FlightStatus == "Cancelled");
            //Assert
            Assert.AreEqual("Cancelled", result[0].FlightStatus);
        }
    }
}
