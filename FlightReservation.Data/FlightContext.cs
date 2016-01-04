using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightReservation.Data
{
    public class FlightContext : DbContext
    {
        public virtual DbSet<Airport> Airports { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<FlightContext>(null);
        }

    }
}
