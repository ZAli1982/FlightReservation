using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Model.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Repositories
{
    public class FlightRepository : BaseRepository, IRepository<Flight>
    {
        public IList<Flight> Get()
        {
            return null; // FakeDatabase.Flights;
        }


        public void Add(Flight flight)
        {
            //FakeDatabase.Flights.Add(flight);
        }


        public void Update(Flight flight)
        {
            var entity = Get().FirstOrDefault(x => x.Id == flight.Id);
            entity.FlightStatus = flight.FlightStatus;
        }


        public Flight GetById(int Id)
        {
            throw new NotImplementedException();
        }


        public void Remove(Flight entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        IList<Flight> IRepository<Flight>.Get()
        {
            throw new NotImplementedException();
        }

        Flight IRepository<Flight>.GetById(int Id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Flight>.Add(Flight entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Flight>.Update(Flight entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Flight>.Remove(Flight entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Flight>.Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
