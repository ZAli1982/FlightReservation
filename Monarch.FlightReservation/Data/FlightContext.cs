using Monarch.FlightReservation.Model.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Data
{
    public class FlightContext : DbContext
    {
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<AirportFlight> AirportFlights { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<TicketOffice> TicketOffices { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<FlightContext>(null);

            // modelBuilder.Configurations.Add(new AuditConfiguration());
        }

    }
}
