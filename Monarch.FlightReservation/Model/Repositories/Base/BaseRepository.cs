using Monarch.FlightReservation.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Repositories.Base
{
    public abstract class BaseRepository 
    {
        public FlightContext Context { get; set; }
    }
}
