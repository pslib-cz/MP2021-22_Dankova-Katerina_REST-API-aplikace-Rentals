using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.DatabaseModel
{
    public enum ItemState
    {
        Rented,
        Available,
        Unavailable,
    }
    public enum RentingState
    {
        InProgress,
        Ended,
        Cancelled,
        WillStart
    }
}
