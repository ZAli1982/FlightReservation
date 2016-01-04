using Monarch.FlightReservation.Helpers;
using Monarch.FlightReservation.Model.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monarch.FlightReservation.Service.Interface
{
    public interface IPromotion
    {
        decimal SeasonDiscount();
        decimal CheckDiscounts();
    }
}
