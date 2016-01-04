using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Model.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Repositories
{
    public class AirportFlightRepository : BaseRepository, IRepository<AirportFlight>
    {
        public IList<AirportFlight> Get()
        {
            return null; // FakeDatabase.AirportFlights;
        }

        public void Add(AirportFlight entity)
        {
            // FakeDatabase.AirportFlights.Add(entity);
        }

        public void Update(AirportFlight entity)
        {
            throw new NotImplementedException();
        }


        public AirportFlight GetById(int Id)
        {
            throw new NotImplementedException();
        }


        public void Remove(AirportFlight entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        IList<AirportFlight> IRepository<AirportFlight>.Get()
        {
            throw new NotImplementedException();
        }

        AirportFlight IRepository<AirportFlight>.GetById(int Id)
        {
            throw new NotImplementedException();
        }

        void IRepository<AirportFlight>.Add(AirportFlight entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<AirportFlight>.Update(AirportFlight entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<AirportFlight>.Remove(AirportFlight entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<AirportFlight>.Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
