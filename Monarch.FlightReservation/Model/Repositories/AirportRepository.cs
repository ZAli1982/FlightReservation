using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Model.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Repositories
{
    public class AirportRepository :BaseRepository, IRepository<Airport> 
    {
        public IList<Airport> Get()
        {
            return null; // FakeDatabase.Airports;
        }

        public void Add(Airport entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Airport entity)
        {
            throw new NotImplementedException();
        }


        public Airport GetById(int Id)
        {
            throw new NotImplementedException();
        }


        public void Remove(Airport entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        IList<Airport> IRepository<Airport>.Get()
        {
            throw new NotImplementedException();
        }

        Airport IRepository<Airport>.GetById(int Id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Airport>.Add(Airport entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Airport>.Update(Airport entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Airport>.Remove(Airport entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Airport>.Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
