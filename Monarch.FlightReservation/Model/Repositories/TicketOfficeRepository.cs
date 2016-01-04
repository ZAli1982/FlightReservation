using Monarch.FlightReservation.Data;
using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Model.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Repositories
{
    public class TicketOfficeRepository : BaseRepository, IRepository<TicketOffice> 
    {
        public TicketOfficeRepository(FlightContext context)
        {
            base.Context = context;
        }

        public IList<TicketOffice> Get()
        {
            return (from to in Context.TicketOffices
                    select to
                    ).ToList();
        }

        public void Add(TicketOffice entity)
        {
            Context.TicketOffices.Add(entity);
            Context.SaveChanges();
        }

        public void Update(TicketOffice entity)
        {
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

        }


        public TicketOffice GetById(int Id)
        {
            return (from to in Context.TicketOffices
                    where to.Id == Id
                    select to
                   ).FirstOrDefault();
        }
        
        public void Remove(TicketOffice entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            //Context.TicketOffices.Remove(entity);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var entityToDelete = Context.TicketOffices.FirstOrDefault(x => x.Id == id);
             Context.Entry(entityToDelete).State = EntityState.Deleted;
             Context.SaveChanges();
        }
    }
}
