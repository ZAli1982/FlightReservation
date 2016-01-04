using Monarch.FlightReservation.Model.Entites;
using System;
using System.Collections.Generic;
namespace Monarch.FlightReservation.Model.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> Get();
        T GetById(int Id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(int id);
    }
}
