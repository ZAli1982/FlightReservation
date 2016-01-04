using Monarch.FlightReservation.Helpers;
using Monarch.FlightReservation.Model.Entites;
using Monarch.FlightReservation.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Service
{
    public class PromotionSales : IPromotion
    {
        private TicketOffice _ticket;
        public PromotionSales(TicketOffice ticket)
        {
            _ticket = ticket;
        }

        public decimal CheckDiscounts()
        { 
         var price = _ticket.Cost;
         price = SeasonDiscount();
         return price;
        }
        public decimal SeasonDiscount()
        {
            decimal totalDiscount = 0;
            if (_ticket.Discount != 0)
            {
                decimal discount = (Convert.ToDecimal(_ticket.Discount) / 100);
               totalDiscount = _ticket.Cost * discount;
            }
            return _ticket.Cost = _ticket.Cost - totalDiscount; 
        }
    }
}
