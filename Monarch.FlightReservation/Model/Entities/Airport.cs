using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Entites
{
    public class Airport
    {
        public int Id { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
    }
}
