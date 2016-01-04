using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Model.Entites
{
    public class TicketOffice
    {
        public int Id { get; set; }
        public int FlightID { get; set; }
        public string TicketType { get; set; }
        public decimal Cost { get; set; }
        public bool Return { get; set; }
        public float Discount { get; set; }
    }
}
