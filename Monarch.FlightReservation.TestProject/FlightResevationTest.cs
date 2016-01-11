using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monarch.FlightReservation.Model.Repositories;
using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Service.Interface;
using Monarch.FlightReservation.Service;
using Monarch.FlightReservation.Helpers;
using Monarch.FlightReservation.Data;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace Monarch.FlightReservation.TestProject
{
    [TestClass]
    public class FlightResevationTest
    {

        Mock<IRepository<Flight>> _mockFlightRepository;
        Mock<IRepository<Airport>> _mockAirportRepository;
        Mock<IRepository<AirportFlight>> _mockAirportFlightRepository;
        Mock<IRepository<TicketOffice>> _mockTicketOfficeRepository;
        IFlightReservationService service;
        [TestInitialize]
        public void SetUp()
        {
            _mockFlightRepository = new Mock<IRepository<Flight>>();
            _mockAirportRepository = new Mock<IRepository<Airport>>();
            _mockAirportFlightRepository = new Mock<IRepository<AirportFlight>>();
            _mockTicketOfficeRepository = new Mock<IRepository<TicketOffice>>();
            service = new FlightReservationService(_mockAirportRepository.Object, _mockFlightRepository.Object, _mockAirportFlightRepository.Object, _mockTicketOfficeRepository.Object);
        }

        [TestMethod]
        public void ShouldOutputListOfFlights()
        {
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());

            var result = service.GetFlightDetail(0);

            Assert.AreEqual(6, result.Count);
        }

        [TestMethod]
        public void ShouldSeeTheNewlyAddedFlightInList()
        {
            //Arrange
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            //_mockFlightRepository.Setup(x => x.Add(It.IsAny<Flight>()));
            //_mockAirportFlightRepository.Setup(x => x.Add(It.IsAny<AirportFlight>()));

            FlightDetail flightDetail = new FlightDetail()
            {
                ArrivalAirport = "Gatwick",
                DepartureAirport = "Luton",
                ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
                DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0)
            };
            //Act
            service.AddFlight(flightDetail);

            //Assert
            _mockFlightRepository.Verify(x => x.Add(It.IsAny<Flight>()));
            _mockAirportFlightRepository.Verify(x => x.Add(It.IsAny<AirportFlight>()));
            //_mockAirportFlightRepository.Verify(x => x.Update(It.IsAny<AirportFlight>()));
        }

        [TestMethod]
        public void ShouldGetOrderedListByDeparturedate()
        {
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights().ToList());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());

            var result = service.GetOrderedFlightDetail("DepartureTime");

            Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2, 12, 0, 0), result[0].DepartureTime);
            Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0), result[1].DepartureTime);
            Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 14, 0, 0), result[2].DepartureTime);
        }

        [TestMethod]
        public void ShouldGetFilterDepatureAirportFlightsBasedSearchOnISOCode()
        {
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights().ToList());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            var result = service.GetDepartureFlight("LTN");

            Assert.AreEqual(2, result.Count);
            //Assert.AreEqual("LTN", result.Contains();
        }

        [TestMethod]
        public void ShouldGetFilterArrivalAirportFlightsBasedSearchOnISOCode()
        {
            //Arrange
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights().ToList());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            //Act
            var result = service.GetArrivalFlight("LGW");
            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void ShouldGetCancelledFlightWhenICancelFlightByFlightNumber()
        {
            const int flightnumber = 1;
            var flightList = Given_A_List_Flights();
            var cancelFlight = flightList.FirstOrDefault(x => x.Id == flightnumber);
            if (cancelFlight == null) Assert.Fail("Flight does not exit");
            cancelFlight.FlightStatus = false;

            _mockFlightRepository.Setup(x => x.Get()).Returns(flightList);
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());

            service.CancelFlight(1);

            service.GetDepartureFlight("LTN");

            _mockFlightRepository.Verify(x => x.Update(It.IsAny<Flight>()));
        }

        [TestMethod]
        public void ShouldGetFutureDepartureFlight()
        {
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights().ToList());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());

            var result = service.GetFutureFlights(true);

            Assert.IsTrue(DateTime.Now < result[0].DepartureTime);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void ShouldGetSeatSoldCounterToIncrementWhenIBuyATicket()
        {
            _mockTicketOfficeRepository.Setup(x => x.Get()).Returns(Given_List_Valid_TicketOffices());
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights().ToList());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());

            int buySeatfForFlight = 3;
            var result = service.PurchaseTicket(buySeatfForFlight);

            Assert.AreEqual(173, result.SoldSeat);
        }

        [TestMethod]
        public void ShouldGetFlightListExcludingFullBookedFlights()
        {
            _mockTicketOfficeRepository.Setup(x => x.Get()).Returns(Given_List_Valid_TicketOffices());
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights().ToList());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());

            var result = service.GetFutureFlights(false);
            Assert.IsTrue(DateTime.Now < result[0].DepartureTime);
            Assert.IsTrue(DateTime.Now < result[1].DepartureTime);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void ShouldGetDiscountToBCNFlight()
        {
            _mockFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Flights());
            _mockTicketOfficeRepository.Setup(x => x.Get()).Returns(Given_List_Valid_TicketOffices());
            _mockAirportFlightRepository.Setup(x => x.Get()).Returns(Given_A_List_Of_Airport_Flights());
            _mockAirportRepository.Setup(x => x.Get()).Returns(Given_A_list_Of_Airports());
            var result = service.PurchaseTicket(5);

            Assert.AreEqual(60, result.Cost);
        }

        [TestMethod]
        public void TestGitHub()
        {
            //Just testing github i djhfisg hfdsiug
        }


        private List<TicketOffice> Given_List_Valid_TicketOffices()
        {
            return new List<TicketOffice> 
            {
                new TicketOffice { Id = 1, FlightID=3, Cost=45, TicketType="ShortHaul", Return=false, Discount=0 },
                new TicketOffice { Id = 1, FlightID=3, Cost=75, TicketType="LongHaul", Return=false, Discount=0 },
                new TicketOffice { Id = 1, FlightID=5, Cost=75, TicketType="LongHaul", Return=false, Discount=20 }
            };
        }

        private List<Flight> Given_A_List_Flights()
        {
            return new List<Flight> 
            {
                new Flight { Id = 1, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0), FlightStatus = true, Aircraft="A320", Seats=175, SoldSeats=175},
                new Flight { Id = 2, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2, 11, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2, 12, 0, 0), FlightStatus = true, Aircraft="A320", Seats=175, SoldSeats=175},
                new Flight { Id = 3, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 2, 10, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 14, 0, 0), FlightStatus = true, Aircraft="A320", Seats=175, SoldSeats=172},
                new Flight { Id = 4, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 21, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 16, 0, 0), FlightStatus = false , Aircraft="A300", Seats=230, SoldSeats=230},
                new Flight { Id = 5, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 14, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 18, 0, 0), FlightStatus = true , Aircraft="A320", Seats=175, SoldSeats=165},
                new Flight { Id = 6, ArrivalTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 22, 0, 0), DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 5, 17, 0, 0), FlightStatus = true , Aircraft="A320", Seats=175, SoldSeats=175}
            };
        }

        private List<Airport> Given_A_list_Of_Airports()
        {
            return new List<Airport> 
                {
                    new Airport { Id = 1, Name = "Luton", IsoCode = "LTN" },
                    new Airport { Id = 2, Name = "Gatwick", IsoCode = "LGW" },
                    new Airport { Id = 3, Name = "Manchester", IsoCode ="MAN" },
                    new Airport { Id = 4, Name = "Amsterdam", IsoCode ="AMS" },
                    new Airport { Id = 5, Name = "Barcelona", IsoCode ="BCN" }
                };
        }

        private List<AirportFlight> Given_A_List_Of_Airport_Flights()
        {
            return new List<AirportFlight> 
                {
                    new AirportFlight { ArriveAirportId = 5, DepartureAirportId = 1, FlightId = 1 },
                    new AirportFlight { ArriveAirportId = 2, DepartureAirportId = 3,  FlightId = 2 },
                    new AirportFlight { ArriveAirportId = 3, DepartureAirportId = 1, FlightId = 3 },
                    new AirportFlight { ArriveAirportId = 2, DepartureAirportId = 3, FlightId = 4 },
                    new AirportFlight { ArriveAirportId = 5, DepartureAirportId = 4, FlightId = 5 },
                    new AirportFlight { ArriveAirportId = 1, DepartureAirportId = 5, FlightId = 6 },
                };
        }

    }
}
